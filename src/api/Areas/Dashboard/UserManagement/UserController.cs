using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portal.Apis.Core.Controllers;
using Portal.Apis.Core.BLL;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Models;
using Microsoft.AspNetCore.Identity;
using Portal.Apis.Core.DAL;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Linq;
using Kendo.DynamicLinq;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Portal.Apis.Core.Configuration;
using System.IO;
using Portal.Apis.Core.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace Portal.Apis.Areas.Dashboard
{
    [Authorize(Policy = Policies.ManageUsers)]
    [Area("Dashboard")]
    [Route("api/{lang?}/dashboard/[controller]")]
    public class UserController : BaseController
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly UserManager<User> _userManager;
        private readonly UserService _userService;
        private IMapper Mapper;

        public UserController(
            IHostingEnvironment appEnvironment,
            UserManager<User> userManager,
            UserService userService,
            IMapper mapper)
        {
            _appEnvironment = appEnvironment;
            _userManager = userManager;
            _userService = userService;
            Mapper = mapper;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            User ent = await _userService.GetUserByIdAsync(id);

            if (ent == null)
                return NotFound();

            var model = Mapper.Map<UserViewModel>(ent);
            model.Roles = await _userService.GetUserRolesAsync(id);

            return Ok(model);
        }

        [HttpGet]
        public virtual IActionResult Get()
        {
            var request = ParseToDataSourceRequest(Request);
            var query = _userService.AllAsQueryable
                .OrderBy("Id desc")
                .AsQueryable();

            (IQueryable<User> data, int total, object aggregates) = query.ApplyQueryState(
                request.Take,
                request.Skip,
                request.Sort,
                request.Filter,
                request.Aggregate
            );

            return Ok(ToDataSourceResult(data, total, aggregates));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserViewModel model, IEnumerable<string> roles)
        {
            if (ModelState.IsValid)
            {
                if (model == null)
                    return BadRequest("User cannot be null");

                if (model.Avatar != null && model.Avatar.IsValid())
                {
                    model.Photo = Path.GetRandomFileName() + Path.GetExtension(model.Avatar.FileName);
                    model.Avatar.Save
                    (
                        model.Photo,
                        Startup.Configuration["Storage:Avatars"],
                        _appEnvironment
                    );
                }

                User user = Activator.CreateInstance<User>();
                Mapper.Map<UserViewModel, User>(model, user);

                var result = await _userManager.CreateAsync(user, "123456");
                if (!result.Succeeded)
                    return BadRequest("Error while creating user");

                user = await _userManager.FindByNameAsync(user.UserName);

                try
                {
                    result = await this._userManager.AddToRolesAsync(user, roles.Distinct());
                }
                catch
                {
                    await _userManager.DeleteAsync(user);
                    throw;
                }

                if (!result.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    return BadRequest("Error while creating user");
                }

                return await GetById(user.Id);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model == null)
                    return BadRequest($"{nameof(model)} cannot be null");

                if (model.Avatar != null && model.Avatar.IsValid())
                {
                    if (!string.IsNullOrEmpty(model.Photo))
                    {
                        if (System.IO.File.Exists(model.Photo))
                            System.IO.File.Delete(model.Photo);
                    }

                    model.Photo = Path.GetRandomFileName() + Path.GetExtension(model.Avatar.FileName);
                    model.Avatar.Save
                    (
                        model.Photo,
                        Startup.Configuration["Storage:Avatars"],
                        _appEnvironment
                    );
                }
                else
                    model.Photo = System.IO.Path.GetFileName(model.Photo);

                User user = await _userManager.FindByIdAsync(model.Id.ToString());

                if (user == null)
                    return NotFound(model.Id);

                Mapper.Map<UserViewModel, User>(model, user);
                var result = await _userManager.UpdateAsync(user);

                if (model.Roles != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var rolesToRemove = userRoles.Except(model.Roles).ToArray();
                    var rolesToAdd = model.Roles.Except(userRoles).Distinct().ToArray();

                    if (rolesToRemove.Any())
                    {
                        result = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                        if (!result.Succeeded)
                            return BadRequest(result.Errors);
                    }

                    if (rolesToAdd.Any())
                    {
                        result = await _userManager.AddToRolesAsync(user, rolesToAdd);
                        if (!result.Succeeded)
                            return BadRequest(result.Errors);
                    }
                }

                if (!result.Succeeded)
                    return BadRequest("Error while updating user");

                return await GetById(user.Id);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
                return NotFound();

            user.Photo = user.Photo.GetFullPath("Storage:Avatars");
            if (!string.IsNullOrEmpty(user.Photo))
            {
                if (System.IO.File.Exists(user.Photo))
                    System.IO.File.Delete(user.Photo);
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }
    }
}