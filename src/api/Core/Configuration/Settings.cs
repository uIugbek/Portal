using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Configuration;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Models;

namespace Portal.Apis.Core.Configuration
{
    public class Settings
    {
        #region Constants

        public const string IS_DELETED_PROPERTY = "IsDeleted";
        public const string Permission = "Permission";
        public const string NoImage = "/images/noimage.png";

        #endregion

        public static IList<CultureViewModel> Cultures
        {
            get
            {
                return Startup.Configuration
                              .GetSection("Globalization:Cultures")
                              .Get<CultureViewModel[]>()
                              .ToList();
            }
        }

        public static string GetCreateQueryTranslationTable(string cultureCode)
        {
            return $"CREATE TABLE translation_{cultureCode}(" +
                        "Id serial PRIMARY KEY," +
                        "Key text NOT NULL," +
                        "Text text NOT NULL)";
        }

        public static class JwtClaimIdentifiers
        {
            public const string Rol = "rol";
            public const string Id = "id";
        }

    }
}