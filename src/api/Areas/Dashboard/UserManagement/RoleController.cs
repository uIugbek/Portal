using System;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Kendo.DynamicLinq;
using Portal.Apis.Core.Controllers;
using Portal.Apis.Core.BLL;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Models;
using Portal.Apis.Core.Configuration;
using Portal.Apis.Core.DAL;

namespace Portal.Apis.Areas.Dashboard
{
    [Area("Dashboard")]
    [Route("api/{lang?}/dashboard/[controller]")]
    public class RoleController : BaseController
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<Role> _roleManager;
        private readonly RoleService _roleService;
        private IMapper Mapper;

        public RoleController(
            ApplicationDbContext dbContext,
            RoleManager<Role> roleManager,
            RoleService roleService,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _roleService = roleService;
            _dbContext = dbContext;
            Mapper = mapper;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            Role ent = await _roleManager.FindByIdAsync(id.ToString());

            if (ent == null)
                return NotFound();

            ent = await _roleService.GetRoleLoadRelatedAsync(ent.Name);
            RoleViewModel model = null;

            if (ent != null)
                model = Mapper.Map<RoleViewModel>(ent);

            return Ok(model);
        }

        [HttpGet]
        public virtual IActionResult Get()
        {
            var request = ParseToDataSourceRequest(Request);
            var query = _roleService.AllAsQueryable
                .OrderBy("Id desc")
                .AsQueryable();

            (IQueryable<Role> data, int total, object aggregates) = query.ApplyQueryState(
                request.Take,
                request.Skip,
                request.Sort,
                request.Filter,
                request.Aggregate
            );

            return Ok(ToDataSourceResult(data, total, aggregates));
        }

        [HttpGet]
        [Route("GetRolesNames")]
        public virtual IActionResult GetRolesNames()
        {
            return Ok(
                _roleService.AllAsQueryable
                            .OrderBy("Id desc")
                            .Select(s=>s.Name)
            );
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model == null)
                    return BadRequest($"{nameof(model)} cannot be null");

                Role role = Mapper.Map<Role>(model);

                var result = await CreateRoleAsync(role, model.Permissions);

                if (result.Item1)
                {
                    role = await _roleManager.FindByNameAsync(role.Name);
                    if (role != null)
                        return await GetById(role.Id);
                }

                AddErrors(result.Item2);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model == null)
                    return BadRequest($"{nameof(model)} cannot be null");

                Role role = await _roleManager.FindByIdAsync(model.Id.ToString());

                if (role == null)
                    return NotFound(model.Id);

                Mapper.Map<RoleViewModel, Role>(model, role);

                var result = await UpdateRoleAsync(role, model.Permissions);
                if (result.Item1)
                    return NoContent();

                AddErrors(result.Item2);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _dbContext.UserRoles.AnyAsync(a => a.RoleId == id))
                return BadRequest("Role cannot be deleted. Remove all users from this role and try again");

            Role role = await _roleManager.FindByIdAsync(id.ToString());

            if (role == null)
                return NotFound();

            var result = await DeleteRoleAsync(role);
            if (!result.Item1)
                throw new Exception("The following errors occurred whilst deleting role: " + string.Join(", ", result.Item2));

            return Ok();
        }

        protected async Task<Tuple<bool, string[]>> CreateRoleAsync(Role role, IEnumerable<string> claims)
        {
            if (claims == null)
                claims = new string[] { };

            string[] invalidClaims = claims.Where(c => Permissions.GetByValue(c) == null).ToArray();
            if (invalidClaims.Any())
                return Tuple.Create(false, new[] { "The following claim types are invalid: " + string.Join(", ", invalidClaims) });

            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
                return Tuple.Create(false, result.Errors.Select(e => e.Description).ToArray());

            role = await _roleManager.FindByNameAsync(role.Name);

            foreach (string claim in claims.Distinct())
            {
                result = await this._roleManager.AddClaimAsync(
                    role,
                    new System.Security.Claims.Claim(
                        Core.Configuration.ClaimTypes.Permission,
                        Permissions.GetByValue(claim)
                    )
                );

                if (!result.Succeeded)
                {
                    await DeleteRoleAsync(role);
                    return Tuple.Create(false, result.Errors.Select(e => e.Description).ToArray());
                }
            }

            return Tuple.Create(true, new string[] { });
        }

        protected async Task<Tuple<bool, string[]>> UpdateRoleAsync(Role role, IEnumerable<string> claims)
        {
            if (claims != null)
            {
                string[] invalidClaims = claims.Where(c => Permissions.GetByValue(c) == null).ToArray();
                if (invalidClaims.Any())
                    return Tuple.Create(
                        false,
                        new[] { "The following claim types are invalid: " + string.Join(", ", invalidClaims) }
                    );
            }

            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
                return Tuple.Create(false, result.Errors.Select(e => e.Description).ToArray());


            if (claims != null)
            {
                var roleClaims = (await _roleManager.GetClaimsAsync(role))
                                                    .Where(c => c.Type == ClaimTypes.Permission);
                var roleClaimValues = roleClaims.Select(c => c.Value).ToArray();

                var claimsToRemove = roleClaimValues.Except(claims).ToArray();
                var claimsToAdd = claims.Except(roleClaimValues).Distinct().ToArray();

                if (claimsToRemove.Any())
                {
                    foreach (string claim in claimsToRemove)
                    {
                        result = await _roleManager.RemoveClaimAsync(
                            role,
                            roleClaims.Where(c => c.Value == claim)
                                      .FirstOrDefault()
                        );
                        if (!result.Succeeded)
                            return Tuple.Create(false, result.Errors.Select(e => e.Description).ToArray());
                    }
                }

                if (claimsToAdd.Any())
                {
                    foreach (string claim in claimsToAdd)
                    {
                        result = await _roleManager.AddClaimAsync(
                            role,
                            new System.Security.Claims.Claim(
                                ClaimTypes.Permission,
                                Permissions.GetByValue(claim)
                            )
                        );
                        if (!result.Succeeded)
                            return Tuple.Create(false, result.Errors.Select(e => e.Description).ToArray());
                    }
                }
            }

            return Tuple.Create(true, new string[] { });
        }

        protected async Task<Tuple<bool, string[]>> DeleteRoleAsync(Role role)
        {
            var result = await _roleManager.DeleteAsync(role);
            return Tuple.Create(result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }

    }
}