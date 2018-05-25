using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Core.Security;

namespace Portal.Apis
{
    public class BaseDbContext<TUser, TRole, TKey> : IdentityDbContext<TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {

        #region Ctor

        public BaseDbContext(IMembershipService<TKey> membershipService, DbContextOptions options)
            : base(options)
        {
            this.MembershipService = membershipService;
        }
        #endregion

        public IMembershipService<TKey> MembershipService { get; }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                                                .Where(x => x.Entity is IAuditableEntity<TKey>
                                                        && (x.State == EntityState.Added
                                                            || x.State == EntityState.Modified));
            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as IAuditableEntity<TKey>;
                if (entity != null)
                {
                    DateTime now = DateTime.Now;

                    if (entry.State == EntityState.Added)
                    {
                        var id = entry.Property("Id");
                        if (id.CurrentValue is Guid)
                            id.CurrentValue = Guid.NewGuid();

                        entity.CreatedBy = MembershipService.UserId;
                        entity.CreatedDate = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }

                    entity.UpdatedBy = MembershipService.UserId;
                    entity.UpdatedDate = now;
                }
            }
            return base.SaveChanges();
        }
    }
}