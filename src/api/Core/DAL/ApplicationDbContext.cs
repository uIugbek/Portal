using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Core.DAL.Mapping;
using Portal.Apis.Models;
using Portal.Apis.Core.Security;

namespace Portal.Apis.Core.DAL
{
    public class ApplicationDbContext : BaseDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(new MembershipService(), options)
        {
        }

        public DbSet<Culture> Cultures { get; set; }

        #region Announcement
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsCategory> NewsCategories { get; set; }
        #endregion

        #region Manual

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Language> Languages { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region AnnouncementMapping
            builder.ApplyConfiguration(new NewsCategoryMap());
            builder.ApplyConfiguration(new NewsMap());
            builder.ApplyConfiguration(new ArticleMap());
            builder.ApplyConfiguration(new ArticleCategoryMap());
            #endregion

            #region CommonMapping
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new CultureMap());
            builder.ApplyConfiguration(new LanguageMap());
            builder.ApplyConfiguration(new RoleMap());
            #endregion

            #region ManualMapping
            builder.ApplyConfiguration(new CityMap());
            builder.ApplyConfiguration(new CountryMap());
            builder.ApplyConfiguration(new RegionMap());
            #endregion
        }
    }
}
