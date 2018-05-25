using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.DAL.Mapping
{
    public class NewsCategoryMap : BaseEntityMap<NewsCategory>
    {
        public override void Configure(EntityTypeBuilder<NewsCategory> builder)
        {
            base.Configure(builder);
            builder.Property(s => s.Name).HasMaxLength(200);
            builder.Property(s => s.Order).HasDefaultValue(0);
            builder.Property(s => s.Code).HasMaxLength(5);            
            builder.Property(s => s.ParentId).IsRequired(false);
            
            builder.HasOne(s => s.ParentCategory)
                .WithMany(s => s.ChildCategories)
                .HasForeignKey(s => s.ParentId).OnDelete(DeleteBehavior.Restrict);            
            builder.HasOne(w => w.City)
                .WithMany(wm=>wm.NewsCategories)
                .HasForeignKey(w => w.CityId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(w => w.Region)
                .WithMany(wm=>wm.NewsCategories)
                .HasForeignKey(w => w.RegionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}