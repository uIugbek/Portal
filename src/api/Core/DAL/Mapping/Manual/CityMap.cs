using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.DAL.Mapping
{
    public class CityMap : BaseEntityMap<City>//IEntityTypeConfiguration<City>
    {
        public override void Configure(EntityTypeBuilder<City> builder)
        {
            base.Configure(builder);
            builder.Property(s => s.Name).HasMaxLength(50);
            builder.Property(s => s.Preview).HasMaxLength(500);
            builder.Property(s => s.Description);
            builder.Property(w => w.Population);
            builder.Property(w => w.Code).HasMaxLength(3);
            builder.Property(w => w.Likes).HasDefaultValue(0);
            builder.Property(w => w.Views).HasDefaultValue(0);
            builder.Property(w => w.Ranking).HasDefaultValue(0);
            builder.HasOne(w => w.Region)
            .WithMany(w => w.Cities)
            .HasForeignKey(w => w.RegionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}