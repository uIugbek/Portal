using System.Collections.Generic;
using Portal.Apis.Core.Attributes;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Models
{
    public class CityViewModel : LocalizableModel<City, City_LocaleViewModel>
    {
        public CityViewModel()
        {
            NewsCategories = new List<NewsCategoryViewModel>();
            News = new List<NewsViewModel>();
            Articles = new List<ArticleViewModel>();
        }

        [Localized]
        public string Name { get; set; }
        [Localized]
        public string Preview { get; set; }
        [Localized]
        public string Description { get; set; }
        public int? Population { get; set; }
        public string Code { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
        public int Ranking { get; set; }
        public int RegionId { get; set; }
        public string PreviewPhotoPath { get; set; }
        public virtual IList<ArticleViewModel> Articles { get; set; }
        public virtual IList<NewsCategoryViewModel> NewsCategories { get; set; }
        public virtual IList<NewsViewModel> News { get; set; }

    }
    public class City_LocaleViewModel : Localizable_LocaleModel
    {
        public string Name { get; set; }
        public string Preview { get; set; }
        public string Description { get; set; }
    }
}