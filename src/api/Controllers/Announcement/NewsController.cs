using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.Apis.Core.Controllers;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Models;
using Portal.Apis.Core.BLL;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Portal.Apis.Core.Extensions;

namespace Portal.Apis.Controllers
{
    [Route("api/{lang?}/[controller]")]
    public class NewsController : BaseController
    {
        private readonly NewsCategoryService _newsCategoryServcive;
        private readonly NewsService Service;
        private readonly IMapper Mapper;
        private readonly TranslateService TranslateService;
        public NewsController(
            NewsCategoryService _newsCategoryServcive,
            NewsService service,
            IMapper mapper,
            TranslateService translateService)
        {
            this.Service = service;
            this.Mapper = mapper;
            this.TranslateService = translateService;
            this._newsCategoryServcive = _newsCategoryServcive;
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            News ent = Service.AllAsQueryable
            .Include(e => e.NewsCategory)
            .Include(e => e.City)
            .Include(e => e.Region)
            .FirstOrDefault(e => e.Id == id);

            if (ent == null)
                return NotFound();

            NewsViewModel model = Activator.CreateInstance<NewsViewModel>();
            model = Mapper.Map<NewsViewModel>(ent);
            model.LoadEntity(ent);
            return Ok(model);
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var query = Service.AllAsQueryable
                    .Include(e => e.Region)
                    .Include(e => e.City)
                    .OrderBy(e => e.Id)
                    .AsQueryable();
                var result = Mapper.Map<IEnumerable<NewsViewModel>>(query).AsQueryable();

                return Ok(result);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("exception", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]NewsViewModel model)
        {
            if (ModelState.IsValid && Validate(model))
            {
                News entity = Activator.CreateInstance<News>();
                entity = Service.AsObjectQuery().AsNoTracking().FirstOrDefault(f => EqualityComparer<int>.Default.Equals(f.Id, model.Id));

                try
                {
                    model.GetKeys(entity);
                    Mapper.Map<NewsViewModel, News>(model, entity);

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

        [HttpGet]
        [Route("GetAsSelect")]
        public IActionResult GetAsSelect()
        {
            Type type = typeof(News);

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

        private bool Validate(NewsViewModel model)
        {
            if (ModelState.IsValid)
                if (model == null)
                    ModelState.AddModelError(string.Empty, "Model is null");

            return ModelState.IsValid;
        }


        [HttpGet]
        [Route("GetTopFour")]
        public IActionResult GetTopFour()
        {
            var entities = Service.AllAsQueryable.OrderByDescending(e => e.Views).Take(4);
            var result = Mapper.Map<IEnumerable<NewsViewModel>>(entities).AsQueryable();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetNewsByCategoryCode")]
        public IActionResult GetNewsByCategoryCode(string CategoryCode, int Skip = 0, int Take = 8)
        {
            var models = Service.AllAsQueryable
                                .Include(e => e.NewsCategory)
                                .Where(e => e.NewsCategory.Code.Equals(CategoryCode))
                                .AsQueryable()
                                .OrderByDescending(e => e.Id)
                                .Skip(Skip)
                                .Take(Take);

            var result = Mapper.Map<IEnumerable<NewsViewModel>>(models).AsQueryable();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetNewsByCategoryId/{categoryId}")]
        public IActionResult GetNewsByCategoryId(int categoryId)
        {
            var models = Service.AllAsQueryable
                        .OrderBy(e => e.Views)
                        .Where(e => e.CategoryId == categoryId)
                        .Take(4);
            var result = Mapper.Map<IEnumerable<NewsViewModel>>(models).AsQueryable();
            return Ok(result);
        }

        [HttpGet]
        [Route("Initialize")]
        public IActionResult Initialize(int Take)
        {
            var models = Service.AllAsQueryable
                        .Include(e => e.Region)
                        .Include(e => e.City)
                        .AsQueryable()
                        .OrderByDescending(e => e.Id)
                        .Take(Take);

            var result = Mapper.Map<IEnumerable<NewsViewModel>>(models).AsQueryable();
            return Ok(result);
        }

        [HttpGet]
        [Route("LoadMore")]
        public IActionResult LoadMore(int Take, int Skip = 0)
        {
            var models = Service.AllAsQueryable
                        .Include(e => e.Region)
                        .Include(e => e.City)
                        .Include(e => e.NewsCategory)
                        .AsQueryable()
                        .OrderByDescending(e => e.Id)
                        .Skip(Skip)
                        .Take(Take);

            var result = Mapper.Map<IEnumerable<NewsViewModel>>(models).AsQueryable();
            return Ok(result);
        }

        [HttpGet]
        [Route("Count")]
        public IActionResult Count(string code = "")
        {
            int count = 0;
            if (code.IsNullOrEmpty())
            {
                count = Service.Count;
            }
            else
            {
                count = Service
                    .AllAsQueryable
                    .Include(e => e.NewsCategory)
                    .Where(e => e.NewsCategory.Code.Equals(code))
                    .Count();
            }

            return Ok(count);
        }

        [HttpGet]
        [Route("UpdateLikes")]
        public IActionResult UpdateLikes(int id, int like)
        {
            var ent = Service.ByID(id);
            if (ent == null)
                return BadRequest();
            if (like == 1)
            {
                ent.Likes++;
            }
            else
            {
                if (ent.Likes > 0)
                    ent.Likes--;
            }

            Service.Update(ent);
            return Ok();
        }

        [HttpGet]
        [Route("UpdateViews")]
        public IActionResult AddViews(int id, int view)
        {
            var ent = Service.ByID(id);
            if (ent == null)
                return BadRequest();

            if (view == 1)
            {
                ent.Views ++;
                Service.Update(ent);
            }
            
            return Ok();
        }
    }

}
