using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.DAL.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName).HasMaxLength(100);
            builder.Property(p => p.LastName).HasMaxLength(100);
            builder.Property(p => p.MiddleName).HasMaxLength(100);
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.UserName).IsRequired();
            builder.Property(p => p.Comment).HasMaxLength(500);
            builder.Property(p => p.Photo).HasMaxLength(250);
            builder.Property(p => p.PostalAddress).HasMaxLength(500);

            builder.HasMany(u => u.Claims).WithOne().HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.Roles).WithOne().HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

        }
    }
}