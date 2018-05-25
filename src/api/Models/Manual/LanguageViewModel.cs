using System;
using Portal.Apis.Core.Attributes;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Models
{
    public class LanguageViewModel : LocalizableModel<Language, Language_LocaleViewModel>
    {
        [Localized] 
        public string Name { get; set; }
        public string Code { get; set; }
    }
    public class Language_LocaleViewModel : Localizable_LocaleModel
    {
        public string Name { get; set; }
    }
}