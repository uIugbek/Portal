using System.Collections.Generic;
using Portal.Apis.Core.Attributes;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Models
{
    public class RegionViewModel : LocalizableModel<Region, Region_LocaleViewModel>
    {

        public RegionViewModel()
        {
            Cities = new HashSet<CityViewModel>();
            NewsCategories = new HashSet<NewsCategoryViewModel>();
            News = new HashSet<NewsViewModel>();
            Articles = new HashSet<ArticleViewModel>();
        }
        [Localized]
        public string Name { get; set; }
        [Localized]
        public string Preview { get; set; }
        [Localized]
        public string Description { get; set; }
        public int? Population { get; set; }
        public string Code { get; set; }
        public int Views { get; set; }
        public int CountryId { get; set; }

        public virtual ICollection<CityViewModel> Cities { get; set; }
        public virtual ICollection<NewsCategoryViewModel> NewsCategories { get; set; }
        public virtual ICollection<ArticleViewModel> Articles { get; set; }
        public virtual ICollection<NewsViewModel> News { get; set; }

    }
    public class Region_LocaleViewModel : Localizable_LocaleModel
    {
        public string Name { get; set; }
        public string Preview { get; set; }
        public string Description { get; set; }
    }
}