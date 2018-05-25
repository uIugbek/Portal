using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portal.Apis.Core.DAL;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.BLL
{
    public class UserService : EntityService<User>
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(
            ApplicationDbContext dbContext,
            IEntityRepository<User> repository
        ) : base(repository)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await AllAsQueryable
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return null;

            return user;
        }

        public async Task<string[]> GetUserRolesAsync(int userId)
        {
            var user = await GetUserByIdAsync(userId);

            var userRoleIds = user.Roles.Select(r => r.RoleId).ToList();

            var roles = await ServiceProvider.GetService<RoleService>()
                                             .AllAsQueryable
                                             .Where(r => userRoleIds.Contains(r.Id))
                                             .Select(r => r.Name)
                                             .ToArrayAsync();

            return roles;
        }
    }
}