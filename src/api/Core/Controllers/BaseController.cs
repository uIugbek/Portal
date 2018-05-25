using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.DynamicLinq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Portal.Apis.Core.Helpers;

namespace Portal.Apis.Core.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            CultureHelper.ChangeCulture(context.RouteData.Values);

            base.OnActionExecuting(context);
        }

        protected virtual DataSourceRequestWithAggregates ParseToDataSourceRequest(HttpRequest request)
        {
            var filter = request.Query.FirstOrDefault(f => f.Key == "filter");

            return filter.Key != null
                ? JsonConvert.DeserializeObject<DataSourceRequestWithAggregates>(filter.Value)
                : new DataSourceRequestWithAggregates();
        }

        protected virtual DataSourceResult ToDataSourceResult<T>(IQueryable<T> query, int total, object aggregates)
        {
            return new DataSourceResult
            {
                Data = query.ToList(),
                Total = total,
                Aggregates = aggregates
            };
        }

        protected void AddErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }
    }
}