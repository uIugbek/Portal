using System;
using Microsoft.AspNetCore.Http;
using Portal.Apis.Core.Attributes;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Models
{
    public class CultureViewModel : LocalizableModel<Culture, Culture_LocaleViewModel>
    {
        [Localized]
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }
        public FileModel File { get; set; }
    }

    public class Culture_LocaleViewModel : Localizable_LocaleModel
    {
        public string Name { get; set; }
    }
}