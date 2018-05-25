using Portal.Apis.Core.Attributes;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Models
{
    public class NewsCategoryViewModel : LocalizableModel<NewsCategory, NewsCategory_LocaleViewModel>
    {
        [Localized]
        public string Name { get; set; }
        public int Order { get; set; }
        public string Code { get; set; }
        public int? ParentId { get; set; }
        public int CityId { get; set; }
        public int RegionId { get; set; }
    }

    public class NewsCategory_LocaleViewModel : Localizable_LocaleModel
    {
        public string Name { get; set; }
    }
}