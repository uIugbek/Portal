using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Portal.Apis.Core.Enums;

namespace Portal.Apis.Core.DAL.Entities
{
    public class User : IdentityUser<int>, IPerson, IEntity<int>
    {
        public User()
        {
        }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Comment { get; set; }
        public Age? Age { get; set; }
        public string Photo { get; set; }
        public string PostalAddress { get; set; }
        public int? ZIP { get; set; }
        public int? CountryId { get; set; }
        public long? FacebookId { get; set; }

        public virtual ICollection<IdentityUserClaim<int>> Claims { get; set; }
        public virtual ICollection<IdentityUserRole<int>> Roles { get; set; }

    }
}
