using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Portal.Apis.Core.Controllers;
using Portal.Apis.Core.BLL;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Models;
using Portal.Apis.Core.Configuration;

namespace Portal.Apis.Areas.Dashboard
{
    [Area("Dashboard")]
    [Authorize(Policy = Policies.ManageCities)]
    [Route("api/{lang?}/dashboard/[controller]")]
    public class CityController : BaseKendoGridApiController<City, CityViewModel, CityService>
    {
        public CityController(CityService service, IMapper mapper, TranslateService translateService)
            : base(service, mapper, translateService)
        {
        }
    }
}