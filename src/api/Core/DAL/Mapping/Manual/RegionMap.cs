using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.DAL.Mapping
{
    public class RegionMap : BaseEntityMap<Region>
    {
        public override void Configure(EntityTypeBuilder<Region> builder)
        {
            base.Configure(builder);
            builder.Property(s => s.Code).HasMaxLength(5);
            builder.Property(s => s.Population);
            builder.Property(s => s.Views).HasDefaultValue(0);
            builder.Property(s => s.Name).HasMaxLength(50);
            builder.Property(s => s.Preview).HasMaxLength(500);
            builder.Property(s => s.Description);
            builder.Property(s => s.CountryId);
        }
    }   
}