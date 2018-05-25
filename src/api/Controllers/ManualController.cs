using System;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Portal.Apis.Core.Enums;
using Portal.Apis.Core.Helpers;
using AutoMapper;
using System.Collections.Generic;
using Portal.Apis.Models;
using Portal.Apis.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Portal.Apis.Core.BLL;
using Microsoft.AspNetCore.Http;
using Portal.Apis.Core.Controllers;

namespace Portal.Apis.Controllers
{
    [Route("api/{lang?}/[controller]")]
    public class ManualController : BaseController
    {
        private RegionService _regionService;
        private ArticleCategoryService _articleCategoryService;
        private NewsCategoryService _newsCategoryService;
        private CityService _cityService;
        private CultureService _cultureService;

        public ManualController(
            RegionService regionService,
            NewsCategoryService newsCategoryService,
            ArticleCategoryService articleCategoryService,
            CityService cityService,
            CultureService cultureService
        )
        {
            _cityService = cityService;
            _regionService = regionService;
            _articleCategoryService = articleCategoryService;
            _newsCategoryService = newsCategoryService;
            _cultureService = cultureService;
        }

        [HttpGet]
        [Route("GetCultures")]
        public IActionResult GetCultures()
        {
            var query = _cultureService.AllAsQueryable.OrderBy(o => o.Id).AsQueryable();
            var result = Mapper.Map<IEnumerable<CultureViewModel>>(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetCitiesByCountry/{id:int}")]
        public IActionResult GetCitiesByCountry(int id)
        {
            var query = _regionService.AllAsQueryable
                                      .Include(s => s.Cities)
                                      .Where(s => s.CountryId == id)
                                      .SelectMany(s => s.Cities);
            var result = Mapper.Map<IEnumerable<CityViewModel>>(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetEnum")]
        public IActionResult GetEnum(string enumName)
        {
            Type type = Assembly.GetExecutingAssembly()
                                .DefinedTypes
                                .FirstOrDefault(s => s.Name == enumName);

            var result = EnumHelper.ToSelectListItems(type);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetArticleCategories")]
        public IActionResult GetGetArticleCategories()
        {
            var query = _articleCategoryService.AllAsQueryable.ToList();
            var result = Mapper.Map<IEnumerable<ArticleCategoryViewModel>>(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetArticleCategories/{id:int}")]
        public IActionResult GetArticleCategories(int id)
        {
            var query = _articleCategoryService.Filter(s => s.Id == id);
            var result = Mapper.Map<IEnumerable<ArticleViewModel>>(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetCities/{id:int}")]
        public IActionResult GetCitiesByRegionId(int id)
        {
            var query = _cityService.Filter(s => s.RegionId == id);
            var result = Mapper.Map<IEnumerable<CityViewModel>>(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetCities")]
        public IActionResult GetCities()
        {
            var query = _cityService.AllAsQueryable.ToList();
            var result = Mapper.Map<IEnumerable<CityViewModel>>(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetNewsCategories")]
        public IActionResult GetNewsCategories()
        {
            var query = _newsCategoryService.AllAsQueryable.ToList();
            var result = Mapper.Map<IEnumerable<NewsCategoryViewModel>>(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetRegionsAsSelect/{id:int}")]
        public IActionResult GetRegionsAsSelect(int id)
        {
            var query = _regionService.Filter(s => s.CountryId == id)
                                      .Select(a => new SelectListItem
                                      {
                                          Value = a.Id,
                                          Text = a.Name.Translate(),
                                          Selected = false
                                      });

            return Ok(query);
        }
    }
}
