using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.BLL
{
    public class RoleService : EntityService<Role>
    {
        public RoleService(IEntityRepository<Role> repository) : base(repository)
        {
        }

        public async Task<Role> GetRoleLoadRelatedAsync(string roleName)
        {
            var role = await AllAsQueryable.Include(r => r.Claims)
                                           .Include(r => r.Users)
                                           .Where(r => r.Name == roleName)
                                           .FirstOrDefaultAsync();

            return role;
        }

    }
}