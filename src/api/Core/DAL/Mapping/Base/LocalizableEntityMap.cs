using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.DAL.Mapping
{
    public class LocalizableEntity_LocaleMap<TEntity, TEntity_Locale> : IEntityTypeConfiguration<TEntity_Locale>
        where TEntity_Locale : BaseEntity, ILocale<TEntity>
        where TEntity : BaseEntity, ILocalizable<TEntity, TEntity_Locale>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity_Locale> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.LocalizableEntity)
                   .WithMany(p => p.Localizations)
                   .HasForeignKey(p => p.LocalizableEntityId);

            builder.HasOne(p => p.Culture)
                   .WithMany()
                   .HasForeignKey(p => p.LocalizableEntityId);
        }
    }
}