using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Portal.Apis.Core.Controllers;
using Portal.Apis.Core.BLL;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Models;
using Portal.Apis.Core.Enums;
using Portal.Apis.Core.Extensions;
using Portal.Apis.Core.Helpers;

namespace Portal.Apis.Areas.Dashboard
{
    [Area("Dashboard")]
    [Route("api/{lang?}/dashboard/[controller]")]
    public class CultureController : BaseKendoGridApiController<Culture, CultureViewModel, CultureService>
    {
        private readonly IHostingEnvironment _appEnvironment;

        public CultureController(
            IHostingEnvironment appEnvironment,
            TranslateService translateService,
            CultureService service,
            IMapper mapper
            )
            : base(service, mapper, translateService)
        {
            _appEnvironment = appEnvironment;
        }

        [HttpPut]
        public override IActionResult Put([FromBody]CultureViewModel model)
        {
            if (ModelState.IsValid)
            {
                Culture entity = Activator.CreateInstance<Culture>();
                entity = Service.AsObjectQuery()
                                .AsNoTracking()
                                .FirstOrDefault(f => EqualityComparer<int>.Default.Equals(f.Id, model.Id));

                if (model.File != null && model.File.IsValid())
                {
                    if (!string.IsNullOrEmpty(model.Icon))
                    {
                        if (System.IO.File.Exists(model.Icon))
                            System.IO.File.Delete(model.Icon);
                    }

                    model.Icon = model.Code + System.IO.Path.GetExtension(model.File.FileName);
                    model.File.Save
                    (
                        model.Icon,
                        Startup.Configuration["Storage:Images"],
                        _appEnvironment
                    );
                }
                else
                    model.Icon = System.IO.Path.GetFileName(model.Icon);

                model.GetKeys(entity);
                Mapper.Map<CultureViewModel, Culture>(model, entity);

                if (Service.TryUpdate(ref entity))
                {
                    model.AfterUpdateEntity(entity);
                    if (!TranslateService.HasTranslationTable(entity.Code))
                        TranslateService.UpdateTranslationTable(entity.Code);
                }

                CultureHelper.ReLoad();

                return Ok(entity);
            }

            return BadRequest(OperationType.Update);
        }

        [HttpPost]
        public override IActionResult Post([FromBody] CultureViewModel model)
        {
            if (ModelState.IsValid)
            {
                Culture entity = Activator.CreateInstance<Culture>();

                if (model.File != null && model.File.IsValid())
                {
                    model.Icon = model.Code + System.IO.Path.GetExtension(model.File.FileName);
                    model.File.Save
                    (
                        model.Icon,
                        Startup.Configuration["Storage:Images"],
                        _appEnvironment
                    );
                }

                model.GenerateKeys();
                entity = Mapper.Map<Culture>(model);

                if (Service.TryCreate(ref entity))
                {
                    model.AfterCreateEntity(entity);
                    if (!TranslateService.HasTranslationTable(model.Code))
                        TranslateService.CreateTranslationTable(entity.Code);
                }

                CultureHelper.ReLoad();

                return Ok(entity);
            }

            return BadRequest(OperationType.Create);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public override IActionResult Delete(int id)
        {
            var entity = Service.ByID(id);

            if (entity == null)
                return NotFound();

            entity.Icon = entity.Icon.GetFullPath("Storage:Images");
            if (!string.IsNullOrEmpty(entity.Icon))
            {
                if (System.IO.File.Exists(entity.Icon))
                    System.IO.File.Delete(entity.Icon);
            }

            CultureViewModel model = Activator.CreateInstance<CultureViewModel>();
            model.GetKeys(entity);

            if (Service.TryDelete(entity))
            {
                model.AfterDeleteEntity(entity);
                if (!TranslateService.HasTranslationTable(model.Code))
                    TranslateService.DeleteTranslationTable(model.Code);
            }

            CultureHelper.ReLoad();

            return Ok(OperationType.Delete);
        }
    }
}