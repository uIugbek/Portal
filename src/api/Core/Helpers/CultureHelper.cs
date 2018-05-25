using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Routing;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis;
using Portal.Apis.Core.Configuration;
using Portal.Apis.Core.BLL;

namespace Portal.Apis.Core.Helpers
{
    public class CultureHelper
    {
        private CultureService _service { get; set; }
        public CultureHelper(CultureService service)
        {
            _service = service;
        }
        
        private static IList<Culture> _cultures;
        public static IList<Culture> Cultures
        {
            get
            {
                if (_cultures == null)
                    _cultures = ServiceProvider.GetService<CultureService>().All.ToList();

                return _cultures;
            }
        }

        public static CultureInfo CurrentCultureInfo
        {
            get { return Thread.CurrentThread.CurrentUICulture; }
        }

        public static string GetDefaultCulture()
        {
            return Startup.Configuration["Globalization:DefaultCulture"];
        }

        private static string _currentCultureCode;
        public static string CurrentCultureCode
        {
            get
            {
                if (string.IsNullOrEmpty(_currentCultureCode))
                    _currentCultureCode = CurrentCultureInfo != null ? CurrentCultureInfo.Name : GetDefaultCulture();
                return _currentCultureCode;
            }
        }

        public static void ChangeCulture(RouteValueDictionary routeValues)
        {
            string cultureName = (routeValues["lang"] != null) ? routeValues["lang"].ToString() : GetDefaultCulture();

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(cultureName);
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(cultureName);

            _currentCultureCode = null;
        }

        public IList<Culture> GetCultures()
        {
            return _service.AllAsQueryable.ToList();
        }

        public static void ReLoad()
        {
            _cultures = null;
        }
    }
}
