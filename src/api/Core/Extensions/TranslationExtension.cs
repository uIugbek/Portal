using System;
using Microsoft.Extensions.DependencyInjection;
using Portal.Apis.Core.BLL;
using Portal.Apis.Core.Helpers;

namespace Portal.Apis.Core.Extensions
{
    public static class TranslationExtension
    {
        public static string Translate(this string key)
        {
            var translateService = ServiceProvider.GetService<TranslateService>();
            
            return translateService.GetTranslation(key);
        }
    }
}