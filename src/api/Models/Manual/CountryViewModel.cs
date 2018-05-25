using System;
using Portal.Apis.Core.Attributes;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Models
{
    public class CountryViewModel : LocalizableModel<Country, Country_LocaleViewModel>
    {
        [Localized] 
        public string Name { get; set; }
        public string Code { get; set; }

    }
    public class Country_LocaleViewModel : Localizable_LocaleModel
    {
        public string Name { get; set; }
    }
}