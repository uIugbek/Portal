using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Models;
using Portal.Apis.Core.BLL;
using Portal.Apis.Core.Helpers;

namespace Portal.Apis.Core.Controllers
{
    public class EntityController<TEntity, TEntityModel, TEntity_LocaleModel, TEntityService> : BaseController
        where TEntity : Entity<int>
        where TEntityModel : LocalizableModel<TEntity, TEntity_LocaleModel>
        where TEntity_LocaleModel : Localizable_LocaleModel
        where TEntityService : EntityService<TEntity>
    {
        protected readonly TEntityService Service;
        protected readonly IMapper Mapper;
        protected readonly TranslateService TranslateService;

        public EntityController(TEntityService service, IMapper mapper, TranslateService translateService)
        {
            Service = service;
            Mapper = mapper;
            TranslateService = translateService;
        }

        [HttpGet]
        public virtual IActionResult Get(object state)
        {
            var query = Service.AllAsQueryable
                               .ToList();

            var result = Mapper.Map<IEnumerable<TEntityModel>>(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public virtual IActionResult GetById(int id)
        {
            TEntity ent = Service.ByID(id);

            if (ent == null)
                return NotFound();

            TEntityModel model = Activator.CreateInstance<TEntityModel>();
            model = Mapper.Map<TEntityModel>(ent);

            return Ok(model);
        }

        [HttpPost]
        public virtual IActionResult Create([FromBody] TEntityModel model)
        {
            if (model == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            model.GenerateKeys();

            TEntity ent = Mapper.Map<TEntity>(model);

            if (Service.TryCreate(ref ent))
                // TranslateService.TranslateLocalizations(model, model.Localizations);
            // model.CreateLocalizations();

            model.AfterCreateEntity(ent);

            return Ok(ent);
        }

        [HttpPut]
        [Route("{id:int}")]
        public virtual IActionResult Update(int id, [FromBody]TEntityModel model)
        {
            // var ent = Service.ByID(id);
            var ent = Service.AsObjectQuery().AsNoTracking().FirstOrDefault(f => f.Id == id);

            if (ent == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            model.GetKeys(ent);
            ent = Mapper.Map<TEntity>(model);

            if (Service.TryUpdate(ref ent))
                TranslateService.UpdateTranslations(model, model.Localizations);
            // model.UpdateLocalizations();

            model.AfterUpdateEntity(ent);

            return Ok(ent);
        }

        // [HttpDelete]
        // [Route("{id:int}")]
        // public virtual IActionResult Delete(int id)
        // {
        //     TEntity ent = Service.ByID(id);

        //     if (ent == null)
        //         return NotFound();

        //     TEntityModel model = Activator.CreateInstance<TEntityModel>();
        //     model.GetKeys(ent);

        //     if (Service.TryDelete(ent))
        //         TranslateService.DeleteTranslations(model);
        //     // model.DeleteLocalizations();

        //     model.AfterDeleteEntity(ent);

        //     return Ok();
        // }
    }
}
