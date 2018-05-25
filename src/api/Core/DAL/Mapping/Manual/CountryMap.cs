using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.DAL.Mapping
{
    public class CountryMap : BaseEntityMap<Country>
    {
        public override void Configure(EntityTypeBuilder<Country> builder)
        {
            base.Configure(builder);
            builder.Property(s => s.Code).HasMaxLength(5);
        }
    }
}