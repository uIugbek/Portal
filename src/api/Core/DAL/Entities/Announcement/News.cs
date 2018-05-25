namespace Portal.Apis.Core.DAL.Entities
{
    public class News : AuditableBaseEntity
    {
        public News()
        {

        }
        public string Name { get; set; }
        public string Preview { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
        public int Ranking { get; set; }
        public string PhotoPath { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }
        public int CategoryId { get; set; }
        public virtual NewsCategory NewsCategory { get; set; }
    }
}