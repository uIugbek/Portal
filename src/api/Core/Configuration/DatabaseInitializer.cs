using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Portal.Apis;
using Portal.Apis.Core.Auth;
using Portal.Apis.Core.BLL;
using Portal.Apis.Core.Configuration;
using Portal.Apis.Core.DAL;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Core.Security;
using Portal.Apis.Models;

namespace Portal.Apis.Core.DAL
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountManager _accountManager;
        private readonly ILogger _logger;
        private readonly TranslateService _translateService;

        public DatabaseInitializer(
            ApplicationDbContext context,
            IAccountManager accountManager,
            ILogger<DatabaseInitializer> logger,
            TranslateService translateService
            )
        {
            _accountManager = accountManager;
            _context = context;
            _logger = logger;
            _translateService = translateService;
        }

        public async Task SeedAsync()
        {
            try
            {
                await _context.Database.MigrateAsync().ConfigureAwait(false);

                foreach (var culture in Settings.Cultures)
                {
                    if (!_translateService.HasTranslationTable(culture.Code))
                    {
                        _translateService.CreateTranslationTable(culture.Code);
                    }
                }

                if (!_context.Cultures.Any())
                {
                    foreach (var culture in Settings.Cultures)
                    {
                        culture.Localizations = Settings.Cultures.Select(s => new Culture_LocaleViewModel
                        {
                            Name = culture.Name,
                            CultureCode = s.Code
                        }).ToList();
                        
                        Culture entity = new Culture();
                        culture.GenerateKeys();
                        Mapper.Map<CultureViewModel, Culture>(culture, entity);

                        _context.Cultures.Add(entity);
                        culture.AfterCreateEntity(entity);
                    }
                    _context.SaveChanges();
                }

                if (!await _context.Users.AnyAsync())
                {
                    _logger.LogInformation("Generating inbuilt accounts");

                    const string adminRoleName = "administrator";
                    const string userRoleName = "user";

                    await EnsureRoleAsync(adminRoleName, "Default administrator", Permissions.GetAll());
                    await EnsureRoleAsync(userRoleName, "Default user", new string[] { });

                    await CreateUserAsync("admin", "123qweR!", "Admin", "Admin", "Admin", "admin@admin.uz", new string[] { adminRoleName });
                    await CreateUserAsync("user", "123qweR!", "User", "User", "User", "user@user.uz", new string[] { userRoleName });

                    _logger.LogInformation("Inbuilt account generation completed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }

        private async Task EnsureRoleAsync(string roleName, string description, string[] claims)
        {
            if ((await _accountManager.GetRoleByNameAsync(roleName)) == null)
            {
                Role applicationRole = new Role
                {
                    Name = roleName,
                    Description = description
                };

                var result = await _accountManager.CreateRoleAsync(applicationRole, claims);

                if (!result.Item1)
                    throw new Exception($"Seeding \"{description}\" role failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");
            }
        }

        private async Task<User> CreateUserAsync(
            string userName,
            string password,
            string firstName,
            string lastName,
            string middleName,
            string email,
            string[] roles
            )
        {
            User applicationUser = new User
            {
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                Email = email,
                EmailConfirmed = true
            };

            var result = await _accountManager.CreateUserAsync(applicationUser, roles, password);

            if (!result.Item1)
                throw new Exception($"Seeding \"{userName}\" user failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");

            return applicationUser;
        }
    }
}