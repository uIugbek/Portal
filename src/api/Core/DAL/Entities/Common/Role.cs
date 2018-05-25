using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Portal.Apis.Core.DAL.Entities
{
    public class Role : IdentityRole<int>, IEntity<int>
    {
        public Role()
        {
        }

        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ICollection<IdentityRoleClaim<int>> Claims { get; set; }
        public virtual ICollection<IdentityUserRole<int>> Users { get; set; }
    }
}
