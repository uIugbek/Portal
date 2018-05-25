using System;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using FluentValidation.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;
using Portal.Apis.Core.DAL;
using Portal.Apis.Models;
using Portal.Apis.Core.SignalR;
using Portal.Apis.Core.Extensions;
using Portal.Apis.Core.Helpers;
using Portal.Apis.Core.BLL;
using Portal.Apis.Core.Configuration;
using Portal.Apis.Core.DAL.Entities;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Portal.Apis
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            LoggerHelper.SetupSerilog();
        }

        public static IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomDbContext();

            services.RegisterServices();

            services.Configure<FacebookAuthSettings>(
                Configuration.GetSection(nameof(FacebookAuthSettings))
            );

            services.Configure<SmtpConfig>(
                Configuration.GetSection("SmtpConfig")
            );

            services.AddCors(option => 
                option.AddPolicy("AllowAll", p => 
                    p.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                )
            );

            services.AddCustomAuthentication();

            services.AddCustomAuthorization();

            services.AddCustomIdentity();

            services.AddSignalR();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", 
                    new Info 
                    { 
                        Title = "Portal API", 
                        Version = "v1" 
                    }
                );
            });

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            services.AddAutoMapper();
 
            services.AddCustomizedMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifeTime)
        {
            ServiceProvider.Services = app.ApplicationServices;

            EmailTemplates.Initialize(env);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        context.Response.AddApplicationError(error.Error.Message);
                        await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                    }
                });
            });

            app.UseAuthentication();

            app.UseCors("AllowAll");

            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".kml"] = "application/vnd.google-earth.kml";
            
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });
            
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(),
                    Startup.Configuration["Storage:KmlFiles"].DirectoryExist(env))
                ),
                RequestPath = "/KmlFiles",
                ContentTypeProvider = provider
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Swagger}/{action=Index}/{id?}"
                );
            });

            app.UseSignalR(routers =>
            {
                routers.MapHub<Chat>("chathub");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            });

        }
    }
}
