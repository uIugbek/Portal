using System;
using Portal.Apis.Core.Auth;

namespace Portal.Apis.Core.Configuration
{
    public static class Policies
    {
        public const string ManageUsers = "Manage All Users";
        public const string ManageRoles = "Manage All Roles";

        #region Announcement Policies

        public const string ManageNews = "Manage All News";
        public const string ManageNewsCategories = "Manage All News Categories";
        public const string ManageArticles = "Manage All Articles";
        public const string ManageArticleCategories = "Manage All Article Categories";
        
        #endregion

        #region Manual Policies
        
        public const string ManageCountries = "Manage All Countries";
        public const string ManageRegions = "Manage All Regions";
        public const string ManageCities = "Manage All Cities";
        
        #endregion
    }
}