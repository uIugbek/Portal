using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Core.Extensions;
using System.Reflection;
using Portal.Apis.Core.BLL;
using Portal.Apis.Core.Helpers;
using Portal.Apis.Core.Attributes;

namespace Portal.Apis.Models
{
    public interface ILocalizableModel<TKey, TEntity, TLocaleModel> : IModel<TKey, TEntity>
        where TKey : struct
        where TEntity : class
        where TLocaleModel : ILocalizable_LocaleModel
    {
        void LoadLocalizations(TEntity ent);

        IList<TLocaleModel> Localizations { get; set; }
    }

    public interface ILocalizable_LocaleModel
    {
        string CultureCode { get; set; }
    }

    public class LocalizableModel<TEntity, TLocaleModel> : Model<TEntity>,  ILocalizableModel<int, TEntity, TLocaleModel>
        where TEntity : class
        where TLocaleModel : ILocalizable_LocaleModel
    {
        public LocalizableModel()
        {
            Localizations = new List<TLocaleModel>();
        }
        
        public override void LoadEntity(TEntity t)
        {
            LoadLocalizations(t);
        }

        public virtual void LoadLocalizations(TEntity ent)
        {
            TranslateService translateService = ServiceProvider.GetService<TranslateService>();

            foreach (var culture in CultureHelper.Cultures)
            {
                TLocaleModel localeModel = Activator.CreateInstance<TLocaleModel>();

                Type entType = ent.GetType();
                Type localeType = localeModel.GetType();
                PropertyInfo[] entProperties = entType.GetProperties();
                PropertyInfo[] localeProperties = localeType.GetProperties();

                foreach (PropertyInfo localeProperty in localeProperties)
                {
                    if (localeProperty.IsStringProperty())
                    {
                        PropertyInfo entProperty = entProperties.FirstOrDefault(w => w.Name == localeProperty.Name);

                        if (entProperty == null)
                            continue;

                        localeProperty.SetValue(
                            localeModel,
                            translateService.GetTranslation(entProperty.GetValue(ent, null).ToString(), culture.Code),
                            null
                        );
                    }
                }

                localeModel.CultureCode = culture.Code;
                Localizations.Add(localeModel);
            }
        }

        public override void AfterCreateEntity(TEntity t)
        {
            Type thisType = GetType();
            PropertyInfo[] thisProperties = thisType.GetProperties();
            TranslateService translateService = ServiceProvider.GetService<TranslateService>();

            foreach (var localization in Localizations)
            {
                Type localization_Type = localization.GetType();
                PropertyInfo[] localization_Properties = localization_Type.GetProperties();

                foreach (PropertyInfo localization_Property in localization_Properties)
                {
                    if (localization_Property.IsStringProperty())
                    {
                        PropertyInfo thisProperty = thisProperties.FirstOrDefault(w => w.Name == localization_Property.Name);

                        if (thisProperty == null)
                            continue;

                        translateService.Translate(
                                        thisProperty.GetValue(this, null)?.ToString(),
                                        localization_Property.GetValue(localization, null)?.ToString(),
                                        localization.CultureCode);
                    }
                }
            }
        }

        public override void AfterUpdateEntity(TEntity t)
        {
            Type thisType = GetType();
            PropertyInfo[] thisProperties = thisType.GetProperties();
            TranslateService translateService = ServiceProvider.GetService<TranslateService>();

            foreach (var localization in Localizations)
            {
                Type localization_Type = localization.GetType();
                PropertyInfo[] localization_Properties = localization_Type.GetProperties();

                foreach (PropertyInfo localization_Property in localization_Properties)
                {
                    if (localization_Property.IsStringProperty())
                    {
                        PropertyInfo thisProperty = thisProperties.FirstOrDefault(w => w.Name == localization_Property.Name);

                        if (thisProperty == null)
                            continue;

                        translateService.UpdateTranslation(
                                        thisProperty.GetValue(this, null).ToString(),
                                        localization_Property.GetValue(localization, null).ToString(),
                                        localization.CultureCode);
                    }
                }
            }
        }

        public override void AfterDeleteEntity(TEntity t)
        {
            Type thisType = GetType();
            PropertyInfo[] thisProperties = thisType.GetProperties();
            TranslateService translateService = ServiceProvider.GetService<TranslateService>();

            foreach (var thisProperty in thisProperties)
            {
                if (thisProperty.IsLocalizedProperty())
                {
                    foreach (var culture in CultureHelper.Cultures)
                    {
                        translateService.DeleteTranslation(
                                        thisProperty.GetValue(this, null).ToString(),
                                        culture.Code);
                    }
                }
            }
        }

        public IList<TLocaleModel> Localizations { get; set; }
    }

    public class Localizable_LocaleModel : ILocalizable_LocaleModel
    {
        [Required]
        public string CultureCode { get; set; }
    }
}
