using System.Collections.Generic;
namespace Portal.Apis.Core.DAL.Entities
{
    public class ArticleCategory : BaseEntity
    {
        public ArticleCategory()
        {
            Articles = new List<Article>();
        }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual IList<Article> Articles { get; set; }
    }
}