using System;
using System.Collections.Generic;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.DAL.Entities
{
    public class Region : BaseEntity
    {
        public Region()
        {
            Cities = new List<City>();
            NewsCategories = new List<NewsCategory>();
            News = new List<News>();
            Articles = new List<Article>();
        }

        public string Name { get; set; }
        public string Preview { get; set; }
        public string Description { get; set; }
        public int? Population { get; set; }
        public string Code { get; set; }
        public int Views { get; set; }
        public int CountryId { get; set; }

        public virtual IList<City> Cities { get; set; }
        public virtual IList<NewsCategory> NewsCategories { get; set; }
        public virtual IList<Article> Articles { get; set; }
        public virtual IList<News> News { get; set; }
    }
}