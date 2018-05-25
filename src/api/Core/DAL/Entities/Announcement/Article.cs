namespace Portal.Apis.Core.DAL.Entities
{
    public class Article : AuditableBaseEntity
    {
        public Article()
        {

        }
        public string Name { get; set; }
        public string Preview { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string PhotoPath { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
        public int Ranking { get; set; }
        public int ArticleCategoryId { get; set; }
        public virtual ArticleCategory ArticleCategory { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }
    }
}