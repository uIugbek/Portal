using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation.AspNetCore;
using Newtonsoft.Json.Serialization;
using AspNet.Security.OpenIdConnect.Primitives;
using Portal.Apis.Models;
using Portal.Apis.Core.DAL;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Core.BLL;
using Portal.Apis.Core.Security;
using Portal.Apis.Core.Auth;
using Portal.Apis.Core.Configuration;
using Portal.Apis.Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using FluentValidation;

namespace Portal.Apis.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH";
        private static readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
        {
            var jwtAppSettingOptions = Startup.Configuration.GetSection(nameof(JwtIssuerOptions));

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            return services;
        }

        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.ManageUsers, policy => policy.RequireClaim(ClaimTypes.Permission, Permissions.USER_CRUD));
                options.AddPolicy(Policies.ManageRoles, policy => policy.RequireClaim(ClaimTypes.Permission, Permissions.ROLE_CRUD));
                options.AddPolicy(Policies.ManageNewsCategories, policy => policy.RequireClaim(ClaimTypes.Permission, Permissions.NEWS_CATEGORY_CRUD));
                options.AddPolicy(Policies.ManageNews, policy => policy.RequireClaim(ClaimTypes.Permission, Permissions.NEWS_CRUD));
                options.AddPolicy(Policies.ManageArticleCategories, policy => policy.RequireClaim(ClaimTypes.Permission, Permissions.ARTICLE_CATEGORY_CRUD));
                options.AddPolicy(Policies.ManageArticles, policy => policy.RequireClaim(ClaimTypes.Permission, Permissions.ARTICLE_CRUD));

                #region Manual
                options.AddPolicy(Policies.ManageCountries, policy => policy.RequireClaim(ClaimTypes.Permission, Permissions.COUNTRY_CRUD));
                options.AddPolicy(Policies.ManageRegions, policy => policy.RequireClaim(ClaimTypes.Permission, Permissions.REGION_CRUD));
                options.AddPolicy(Policies.ManageCities, policy => policy.RequireClaim(ClaimTypes.Permission, Permissions.CITY_CRUD));
                #endregion
            });


            return services;
        }

        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services)
        {
            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(Startup.Configuration["Data:PostgreSqlConnectionString"]);
            });

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(DbContext), typeof(BaseDbContext<User, Role, int>));
            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
            services.AddScoped<IAccountManager, AccountManager>();

            #region Common

            services.AddScoped<TranslateService>();
            services.AddScoped<CultureService>();
            services.AddScoped<CountryService>();
            services.AddScoped<RegionService>();
            services.AddScoped<CityService>();
            services.AddScoped<UserService>();
            services.AddScoped<RoleService>();

            #endregion

            services.AddScoped<ArticleCategoryService>();
            services.AddScoped<ArticleService>();
            services.AddScoped<NewsCategoryService>();
            services.AddScoped<NewsService>();

            services.AddScoped(typeof(IMembershipService<int>), typeof(MembershipService));
            services.AddScoped<IEmailer, Emailer>();

            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<RoleManager<Role>>();
            services.AddScoped<IJwtFactory, JwtFactory>();

            return services;
        }

        public static IServiceCollection AddCustomizedMvc(this IServiceCollection services)
        {
            services.AddMvc()
                    .AddControllersAsServices()
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling =
                            Newtonsoft.Json.ReferenceLoopHandling.Ignore;

                        options.SerializerSettings.ContractResolver =
                            new CamelCasePropertyNamesContractResolver();

                        options.SerializerSettings.PreserveReferencesHandling =
                            Newtonsoft.Json.PreserveReferencesHandling.None;

                        options.SerializerSettings.DateTimeZoneHandling =
                            Newtonsoft.Json.DateTimeZoneHandling.Local;
                    })
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            return services;
        }

    }
}