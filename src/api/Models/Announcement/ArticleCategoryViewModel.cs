using Portal.Apis.Core.Attributes;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Models
{
    public class ArticleCategoryViewModel : LocalizableModel<ArticleCategory, ArticleCategory_LocaleViewModel>
    {
        [Localized]
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class ArticleCategory_LocaleViewModel : Localizable_LocaleModel
    {
        public string Name { get; set; }
    }
}