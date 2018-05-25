using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http;
using System.Reflection;
using Newtonsoft.Json;
using Kendo.DynamicLinq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.WebUtilities;
using AutoMapper;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Core.BLL;
using Portal.Apis.Models;
using Portal.Apis.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Apis.Core.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Portal.Apis.Core.Controllers
{
    public abstract class BaseKendoGridApiController<TEntity, TEntityModel, TEntityService> : BaseKendoGridApiController<int, TEntity, TEntityModel, TEntityService>
        where TEntity : class, IEntity<int>
        where TEntityModel : class, IModel<int, TEntity>
        where TEntityService : IEntityService<TEntity>
    {
        public BaseKendoGridApiController(TEntityService service, IMapper mapper, TranslateService translateService)
            : base(service, mapper, translateService)
        {
        }
    }

    public abstract class BaseKendoGridApiController<TKey, TEntity, TEntityModel, TEntityService> : BaseController
    where TKey : struct
    where TEntity : class, IEntity<TKey>
    where TEntityModel : class, IModel<TKey, TEntity>
    where TEntityService : IEntityService<TEntity>
    {
        protected readonly TEntityService Service;
        protected readonly IMapper Mapper;
        protected readonly TranslateService TranslateService;
        protected string OrderByProperty = "Id";
        protected string OrderType = "desc";

        #region Ctor

        public BaseKendoGridApiController(TEntityService service, IMapper mapper, TranslateService translateService)
            : base()
        {
            Service = service;
            Mapper = mapper;
            TranslateService = translateService;
        }

        #endregion

        [HttpGet]
        [Route("{id:int}")]
        public virtual IActionResult GetById(TKey id)
        {
            TEntity ent = Service.ByID(id);

            if (ent == null)
                return NotFound();

            try
            {
                TEntityModel model = Activator.CreateInstance<TEntityModel>();
                model = Mapper.Map<TEntityModel>(ent);
                model.LoadEntity(ent);

                return Ok(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("exception", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        public virtual IActionResult Get()
        {
            try
            {
                var request = ParseToDataSourceRequest(Request);
                var query = Service.AllAsQueryable
                    .OrderBy(string.Format("{0} {1}", OrderByProperty, OrderType))
                    .AsQueryable();
                var result = Mapper.Map<IEnumerable<TEntityModel>>(query).AsQueryable();

                (IQueryable<TEntityModel> data, int total, object aggregates) = result.ApplyQueryState(
                    request.Take,
                    request.Skip,
                    request.Sort,
                    request.Filter,
                    request.Aggregate
                );

                return Ok(ToDataSourceResult(data, total, aggregates));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("exception", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public virtual IActionResult Put([FromBody]TEntityModel model)
        {
            if (ModelState.IsValid && Validate(model))
            {
                TEntity entity = Activator.CreateInstance<TEntity>();
                entity = Service.AsObjectQuery().AsNoTracking().FirstOrDefault(f => EqualityComparer<TKey>.Default.Equals(f.Id, model.Id));

                try
                {
                    model.GetKeys(entity);
                    Mapper.Map<TEntityModel, TEntity>(model, entity);

                    if (Service.TryUpdate(ref entity))
                        model.AfterUpdateEntity(entity);

                    return Ok(entity);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("exception", ex.Message);
                    return BadRequest(ModelState);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public virtual IActionResult Post([FromBody] TEntityModel model)
        {
            if (ModelState.IsValid && Validate(model))
            {
                TEntity entity = Activator.CreateInstance<TEntity>();

                try
                {
                    model.GenerateKeys();
                    entity = Mapper.Map<TEntity>(model);

                    if (Service.TryCreate(ref entity))
                        model.AfterCreateEntity(entity);

                    return Ok(entity);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("exception", ex.Message);
                    return BadRequest(ModelState);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public virtual IActionResult Delete(TKey id)
        {
            var entity = Service.ByID(id);

            if (entity == null)
                return NotFound();

            try
            {
                TEntityModel model = Activator.CreateInstance<TEntityModel>();
                model.GetKeys(entity);

                if (Service.TryDelete(entity))
                    model.AfterDeleteEntity(entity);

                return Ok();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("exception", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAsSelect")]
        public virtual IActionResult GetAsSelect()
        {
            Type type = typeof(TEntity);

            if (type.GetProperties().Any(s => s.Name == "Name") && type.GetProperties().Any(s => s.Name == "Id"))
            {
                var result = Service.AllAsQueryable.Select(s => new
                {
                    Value = type.GetProperty("Id").GetValue(s),
                    Text = type.GetProperty("Name").GetValue(s).ToString().Translate(),
                    Selected = false
                });
                return Ok(result);
            }

            return BadRequest(ModelState);
        }

        protected virtual bool Validate(TEntityModel model)
        {
            if (ModelState.IsValid)
                if (model == null)
                    ModelState.AddModelError(string.Empty, "Model is null");

            return ModelState.IsValid;
        }

        protected override DataSourceResult ToDataSourceResult<T>(IQueryable<T> query, int total, object aggregates)
        {
            return new DataSourceResult
            {
                Data = Mapper.Map<IEnumerable<TEntityModel>>(query).ToList(),
                Total = total,
                Aggregates = aggregates
            };
        }
    }
}