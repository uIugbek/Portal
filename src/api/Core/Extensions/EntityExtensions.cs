using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using NpgsqlTypes;
using Portal.Apis.Core.Configuration;

namespace Portal.Apis.Core.Extensions
{
    public static class EntityExtensions
    {
        public static void MarkAsDeleted(this object entity,
                                          Type typeOfEntity = null,
                                          PropertyInfo isDeletedProperty = null,
                                          Type[] notInculedEntityTypes = null)
        {
            if (entity == null)
                return;

            if (typeOfEntity == null)
                typeOfEntity = entity.GetType();

            if (notInculedEntityTypes != null && notInculedEntityTypes.Any(a => a.Name == typeOfEntity.Name))
                return;

            if (isDeletedProperty == null)
                isDeletedProperty = typeOfEntity.GetProperty(Settings.IS_DELETED_PROPERTY);

            if (isDeletedProperty == null)
                return;

            isDeletedProperty.SetValue(entity, true, null);

            #region Check Has Child Entity

            var entityCollectionProps = typeOfEntity.GetProperties()
                .Where(a => a.PropertyType.Name.Contains("ICollection"));
            Type typeOfChildEnt = null;
            PropertyInfo isDeletedPropOfChild = null;

            foreach (var entityCollectionProp in entityCollectionProps)
            {
                var entCollections = entityCollectionProp.GetValue(entity, null) as IEnumerable;
                if (entCollections == null)
                    continue;

                typeOfChildEnt = IEnumerableExtensions.GetEnumerableElementType(entityCollectionProp.PropertyType);
                isDeletedPropOfChild = typeOfChildEnt.GetProperty(Settings.IS_DELETED_PROPERTY);
                if (notInculedEntityTypes != null && notInculedEntityTypes.Any(a => a.Name == typeOfChildEnt.Name))
                    continue;

                foreach (var ent in entCollections)
                {
                    ent.MarkAsDeleted(typeOfChildEnt, isDeletedPropOfChild, notInculedEntityTypes);
                }
            }

            #endregion
        }

        public static void UnMarkAsDeleted(this object entity,
                                          Type typeOfEntity = null,
                                          PropertyInfo isDeletedProperty = null,
                                          Type[] notInculedEntityTypes = null)
        {
            if (entity == null)
                return;

            if (typeOfEntity == null)
                typeOfEntity = entity.GetType();

            if (notInculedEntityTypes != null && notInculedEntityTypes.Any(a => a.Name == typeOfEntity.Name))
                return;

            if (isDeletedProperty == null)
                isDeletedProperty = typeOfEntity.GetProperty(Settings.IS_DELETED_PROPERTY);

            if (isDeletedProperty == null)
                return;

            isDeletedProperty.SetValue(entity, false, null);

            #region Check Has Child Entity

            var entityCollectionProps = typeOfEntity.GetProperties()
                .Where(a => a.PropertyType.Name.Contains("ICollection"));

            Type typeOfChildEnt = null;
            PropertyInfo isDeletedPropOfChild = null;

            foreach (var entityCollectionProp in entityCollectionProps)
            {
                var entCollections = entityCollectionProp.GetValue(entity, null) as IEnumerable;
                if (entCollections == null)
                    continue;

                typeOfChildEnt = IEnumerableExtensions.GetEnumerableElementType(entityCollectionProp.PropertyType);
                isDeletedPropOfChild = typeOfChildEnt.GetProperty(Settings.IS_DELETED_PROPERTY);
                if (notInculedEntityTypes != null && notInculedEntityTypes.Any(a => a.Name == typeOfChildEnt.Name))
                    continue;

                foreach (var ent in entCollections)
                {
                    ent.UnMarkAsDeleted(typeOfChildEnt, isDeletedPropOfChild, notInculedEntityTypes);
                }
            }

            #endregion
        }

        public static string GetFullPath(this string filename, string path)
        {
            string filePath = String.IsNullOrEmpty(filename)
                ? Settings.NoImage
                : ("/" + Startup.Configuration[path].Replace("wwwroot/", "") + "/" + filename);
            return Startup.Configuration["DomainName"] + filePath;



        }

    }
}