using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Apis.Core.Configuration
{
    public static class Permissions
    {
        public const string USER_CRUD = "USER_CRUD";
        public const string ROLE_CRUD = "ROLE_CRUD";

        #region Announcement

        public const string NEWS_CATEGORY_CRUD = "NEWS_CATEGORY_CRUD";
        public const string NEWS_CRUD = "NEWS_CRUD";
        public const string ARTICLE_CATEGORY_CRUD = "ARTICLE_CATEGORY_CRUD";
        public const string ARTICLE_CRUD = "ARTICLE_CRUD";

        #endregion

        #region Manual Permissions

        public const string COUNTRY_CRUD = "COUNTRY_CRUD";
        public const string REGION_CRUD = "REGION_CRUD";
        public const string CITY_CRUD = "CITY_CRUD";

        #endregion

        public static string[] GetAll()
        {
            return new string[]{
                USER_CRUD,
                ROLE_CRUD,
                NEWS_CATEGORY_CRUD,
                NEWS_CRUD,
                ARTICLE_CATEGORY_CRUD,
                ARTICLE_CRUD,
                COUNTRY_CRUD,
                REGION_CRUD,
                CITY_CRUD
            };
        }

        public static string GetByValue(string value)
        {
            return GetAll().FirstOrDefault(f => f == value);
        }
    }
}