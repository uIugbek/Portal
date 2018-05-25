using System;
using System.Collections.Generic;
using System.Linq;
using Portal.Apis.Core.Helpers;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.Extensions
{
    public static class LocalizationExtensions
    {
        // public static TEntity_Locale GetLocale<TEntity, TEntity_Locale>(this ILocalizable<TEntity, TEntity_Locale> localizable, int cultureId)
        //     where TEntity_Locale : BaseEntity, ILocale<TEntity>
        //     where TEntity : BaseEntity, ILocalizable<TEntity,TEntity_Locale>
        // {
        //     return localizable != null ? localizable.Localizations.SingleOrDefault(a => a.CultureId == cultureId) : Activator.CreateInstance<TEntity_Locale>();
        // }

        // public static TEntity_Locale GetLocale<TEntity, TEntity_Locale>(this ILocalizable<TEntity,TEntity_Locale> localizable, string cultureName)
        //     where TEntity_Locale : BaseEntity, ILocale<TEntity>
        //     where TEntity : BaseEntity, ILocalizable<TEntity,TEntity_Locale>
        // {
        //     int cultureId = CultureHelper.Cultures.SingleOrDefault(a => a.Code == cultureName).Id;
        //     return localizable.GetLocale(cultureId);
        // }

        // public static TEntity_Locale GetCurrentLocale<TEntity, TEntity_Locale>(this ILocalizable<TEntity, TEntity_Locale> localizable)
        //     where TEntity_Locale : BaseEntity, ILocale<TEntity>
        //     where TEntity : BaseEntity, ILocalizable<TEntity,TEntity_Locale>
        // {
        //     return localizable.GetLocale(CultureHelper.CurrentCultureId);
        // }
    }
}