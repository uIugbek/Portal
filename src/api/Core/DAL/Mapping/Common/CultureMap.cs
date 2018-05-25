using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.DAL.Mapping
{
    public class CultureMap : IEntityTypeConfiguration<Culture>
    {
        public void Configure(EntityTypeBuilder<Culture> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Code).HasMaxLength(10).IsRequired();
            builder.Property(p => p.Name).HasMaxLength(250);
        }
    }
}