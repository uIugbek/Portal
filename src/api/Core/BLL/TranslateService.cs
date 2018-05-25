using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Portal.Apis.Core.Configuration;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Core.Extensions;
using Portal.Apis.Core.Helpers;
using Portal.Apis.Models;

namespace Portal.Apis.Core.BLL
{
    public class TranslateService
    {
        private readonly string _connectionString = Startup.Configuration["Data:NpgsqlConnectionString"];
        private readonly string _currentCultureCode = CultureHelper.CurrentCultureCode;

        public string GetTranslation(string key)
        {
            string result = string.Empty;

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.CommandText = $"SELECT text FROM translation_{_currentCultureCode} WHERE key = '{key}'";
                    command.Connection = connection;
                    NpgsqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result = reader.GetString(0);
                        }
                    }
                }
                catch //(SqlException ex)
                {
                    // logger.LogError(ex, "An error occurred while connecting database.");
                }
                finally
                {
                    connection.Close();
                }
            }

            return result;
        }

        public string GetTranslation(string key, string culture)
        {
            string result = string.Empty;

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.CommandText = $"SELECT text FROM translation_{culture} WHERE key = '{key}'";
                    command.Connection = connection;
                    NpgsqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result = reader.GetString(0);
                        }
                    }
                }
                catch //(SqlException ex)
                {
                    // logger.LogError(ex, "An error occurred while connecting database.");
                }
                finally
                {
                    connection.Close();
                }
            }

            return result;
        }

        public void Translate(string key, string text, string cultureCode)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    text = text.Replace("'", "''");
                    command.CommandText = $"INSERT INTO translation_{cultureCode}(key, text) VALUES ('{key}', '{text}')";
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
                catch //(SqlException ex)
                {
                    // logger.LogError(ex, "An error occurred while connecting database.");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdateTranslation(string key, string text, string cultureCode)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    text = text.Replace("'", "''");
                    command.Connection = connection;
                    command.CommandText = $"SELECT COUNT(*) FROM translation_{cultureCode} WHERE key = '{key}'";

                    var res = (long)command.ExecuteScalar();
                    command.Parameters.Clear();

                    if (res > 0)
                        command.CommandText = $"UPDATE translation_{cultureCode} SET text = '{text}' WHERE key = '{key}'";
                    else
                        command.CommandText = $"INSERT INTO translation_{cultureCode}(key, text) VALUES ('{key}', '{text}')";

                    command.ExecuteNonQuery();
                }
                catch //(SqlException ex)
                {
                    // logger.LogError(ex, "An error occurred while connecting database.");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void DeleteTranslation(string key, string cultureCode)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.CommandText = $"DELETE FROM translation_{cultureCode} WHERE key = '{key}'";
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
                catch //(SqlException ex)
                {
                    // logger.LogError(ex, "An error occurred while connecting database.");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void TranslateLocalizations<TKey, TEntity, TEntityModel, TEntity_LocaleModel>(TEntityModel model, IList<TEntity_LocaleModel> localizations)
            where TKey : struct
            where TEntity : class
            where TEntity_LocaleModel : ILocalizable_LocaleModel
            where TEntityModel : ILocalizableModel<TKey, TEntity, TEntity_LocaleModel>
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    Type thisType = model.GetType();
                    PropertyInfo[] thisProperties = thisType.GetProperties();

                    foreach (var localization in localizations)
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

                                using (NpgsqlCommand command = new NpgsqlCommand())
                                {
                                    var key = thisProperty.GetValue(model, null).ToString();
                                    var text = localization_Property.GetValue(localization, null).ToString();
                                    command.CommandText = $"INSERT INTO translation_{localization.CultureCode}(key, text) VALUES ('{key}', '{text}')";
                                    command.Connection = connection;
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                catch //(SqlException ex)
                {
                    // logger.LogError(ex, "An error occurred while connecting database.");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdateTranslations<TEntityModel, TEntity_LocalModel>(TEntityModel model, IList<TEntity_LocalModel> localizations)
            where TEntityModel : class
            where TEntity_LocalModel : Localizable_LocaleModel
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    Type thisType = model.GetType();
                    PropertyInfo[] thisProperties = thisType.GetProperties();

                    foreach (var localization in localizations)
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

                                using (NpgsqlCommand command = new NpgsqlCommand())
                                {
                                    var key = thisProperty.GetValue(model, null).ToString();
                                    var text = localization_Property.GetValue(localization, null).ToString();
                                    command.Connection = connection;
                                    command.CommandText = $"SELECT COUNT(*) FROM translation_{localization.CultureCode} WHERE key = '{key}'";

                                    var res = (long)command.ExecuteScalar();
                                    command.Parameters.Clear();

                                    if (res > 0)
                                        command.CommandText = $"UPDATE translation_{localization.CultureCode} SET text = '{text}' WHERE key = '{key}'";
                                    else
                                        command.CommandText = $"INSERT INTO translation_{localization.CultureCode}(key, text) VALUES ('{key}', '{text}')";

                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                catch //(SqlException ex)
                {
                    // logger.LogError(ex, "An error occurred while connecting database.");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void DeleteTranslations<TKey, TEntity, TEntityModel, TEntity_LocaleModel>(TEntityModel model)
            where TKey : struct
            where TEntity : class
            where TEntity_LocaleModel : ILocalizable_LocaleModel
            where TEntityModel : ILocalizableModel<TKey, TEntity, TEntity_LocaleModel>
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    Type modelType = model.GetType();
                    PropertyInfo[] modelProperties = modelType.GetProperties();

                    foreach (var modelProperty in modelProperties)
                    {
                        if (modelProperty.IsLocalizedProperty())
                        {
                            foreach (var culture in CultureHelper.Cultures)
                            {
                                using (NpgsqlCommand command = new NpgsqlCommand())
                                {
                                    var key = modelProperty.GetValue(model, null).ToString();
                                    command.CommandText = $"DELETE FROM translation_{culture.Code} WHERE key = '{key}'";
                                    command.Connection = connection;
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                catch //(SqlException ex)
                {
                    // logger.LogError(ex, "An error occurred while connecting database.");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void CreateTranslationTable(string cultureCode)
        {
            if (!HasTranslationTable(cultureCode))
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();

                        NpgsqlCommand command = new NpgsqlCommand();
                        command.CommandText = Settings.GetCreateQueryTranslationTable(cultureCode);
                        command.Connection = connection;
                        command.ExecuteNonQuery();
                    }
                    catch //(SqlException ex)
                    {
                        // logger.LogError(ex, "An error occurred while connecting database.");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public void UpdateTranslationTable(string cultureCode)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.CommandText = $"ALTER TABLE translation_{cultureCode} RENAME TO translation_{cultureCode};";
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
                catch //(SqlException ex)
                {
                    // logger.LogError(ex, "An error occurred while connecting database.");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void DeleteTranslationTable(string cultureCode)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.CommandText = $"DROP TABLE translation_{cultureCode}";
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
                catch //(SqlException ex)
                {
                    // logger.LogError(ex, "An error occurred while connecting database.");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool HasTranslationTable(string cultureCode)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                long t = 0;
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.CommandText = $"SELECT count(*) FROM pg_catalog.pg_tables where schemaname='public' and tablename='translation_{cultureCode}'";
                    command.Connection = connection;
                    t = (long)command.ExecuteScalar();

                }
                catch //(SqlException ex)
                {
                    // logger.LogError(ex, "An error occurred while connecting database.");
                    return false;
                }
                finally
                {
                    connection.Close();
                }

                return t > 0;
            }
        }
    }
}