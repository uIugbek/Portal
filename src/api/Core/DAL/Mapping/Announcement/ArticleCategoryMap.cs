using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.DAL.Mapping
{
    public class ArticleCategoryMap : BaseEntityMap<ArticleCategory>
    {
        public override void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            base.Configure(builder);
            builder.Property(s => s.Name).HasMaxLength(100);
            builder.Property(w => w.Code).HasMaxLength(5);
        }
    }
}