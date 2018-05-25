using Portal.Apis.Core.Attributes;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Models
{
    public class NewsClientViewModel : LocalizableModel<News, NewsClient_LocaleViewModel>
    {
        [Localized]
        public string Name { get; set; }
        [Localized]
        public string Preview { get; set; }
        [Localized]
        public string Description { get; set; }
        [Localized]
        public string Source { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
        public int Ranking { get; set; }
        [Localized]
        public string CityName { get; set; }
        [Localized]
        public string RegionName { get; set; }
        [Localized]
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }

    public class NewsClient_LocaleViewModel : Localizable_LocaleModel
    {
        public string Name { get; set; }
        public string Preview { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string CategoryName { get; set; }            
    }
}