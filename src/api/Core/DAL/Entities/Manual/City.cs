using System;
using System.Collections.Generic;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.DAL.Entities
{
    public class City : BaseEntity
    {
        public City()
        {
            NewsCategories = new List<NewsCategory>();
            News = new List<News>();
            Articles = new List<Article>();
        }

        public string Name { get; set; }
        public string Preview { get; set; }
        public string Description { get; set; }
        public int? Population { get; set; }
        public string Code { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
        public int Ranking { get; set; }
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }

        public virtual IList<Article> Articles { get; set; }
        public virtual IList<NewsCategory> NewsCategories { get; set; }
        public virtual IList<News> News { get; set; }

    }
}