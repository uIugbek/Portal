using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.DAL.Mapping
{
    public class LanguageMap : BaseEntityMap<Language>
    {
        public override void Configure(EntityTypeBuilder<Language> builder)
        {
            base.Configure(builder);
            builder.Property(w => w.Code).HasMaxLength(5);
            builder.Property(w => w.Name).HasMaxLength(50);
        }
    }

}