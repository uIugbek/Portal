using System.Collections.Generic;

namespace Portal.Apis.Core.DAL.Entities
{
    public class NewsCategory : BaseEntity
    {
        public NewsCategory()
        {
            this.ChildCategories = new List<NewsCategory>();
            this.News = new List<News>();
        }
        public string Name { get; set; }
        public int Order { get; set; }
        public string Code { get; set; }
        public int? ParentId { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }
        public virtual NewsCategory ParentCategory { get; set; }
        public virtual IList<NewsCategory> ChildCategories { get; set; }
        public virtual IList<News> News { get; set; }        
    }
}