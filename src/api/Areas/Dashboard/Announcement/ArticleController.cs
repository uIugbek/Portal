using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Portal.Apis.Core.Controllers;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Models;
using Portal.Apis.Core.BLL;
using Portal.Apis.Core.Configuration;

namespace Portal.Apis.Areas.Dashboard
    {
        [Area("Dashboard")]
        [Authorize(Policy = Policies.ManageArticles)]
        [Route("api/{lang?}/dashboard/[controller]")]
        public class ArticleController : BaseKendoGridApiController<Article, ArticleViewModel, ArticleService>
        {
            public ArticleController(ArticleService service, IMapper mapper, TranslateService translateService) : base(service, mapper, translateService)
            {

            }
        }
    }