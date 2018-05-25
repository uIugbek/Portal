using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.DAL.Mapping
{
    public class ArticleMap : BaseEntityMap<Article>
    {
        public override void Configure(EntityTypeBuilder<Article> builder)
        {
            base.Configure(builder);
            builder.Property(s => s.Name).HasMaxLength(200);
            builder.Property(s => s.Preview).HasMaxLength(400);
            builder.Property(s => s.Description);
            builder.Property(w => w.Source).HasMaxLength(200);
            builder.Property(w => w.PhotoPath).HasMaxLength(200);
            builder.Property(w => w.Likes).HasDefaultValue(0);
            builder.Property(w => w.Views).HasDefaultValue(0);
            builder.Property(w => w.Ranking).HasDefaultValue(0);

             builder.HasOne(w => w.ArticleCategory)
                .WithMany(wm=>wm.Articles)
                .HasForeignKey(w => w.ArticleCategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(w => w.City)
                .WithMany(wm=>wm.Articles)
                .HasForeignKey(w => w.CityId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(w => w.Region)
                .WithMany(wm=>wm.Articles)
                .HasForeignKey(w => w.RegionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}