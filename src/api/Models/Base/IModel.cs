using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Core.Extensions;

namespace Portal.Apis.Models
{
    public interface IModel<TKey, TEntity>
        where TKey : struct
        where TEntity : class
    {
        TKey Id { get; set; }

        void Initialize();

        void LoadEntity(TEntity t);

        void GenerateKeys();

        void GetKeys(TEntity ent);

        void AfterCreateEntity(TEntity t);

        void AfterUpdateEntity(TEntity t);

        void AfterDeleteEntity(TEntity t);
    }

    public class Model<TEntity> : IModel<int, TEntity>
        where TEntity : class
    {
        public int Id { get; set; }

        public Model()
        {
        }

        public Model(TEntity ent)
            : this()
        {
            if (ent != null)
                LoadEntity(ent);
        }

        public virtual void Initialize()
        {
        }

        public virtual void LoadEntity(TEntity t)
        {
        }

        public virtual void GenerateKeys()
        {
            Type thisType = GetType();
            PropertyInfo[] thisProperties = thisType.GetProperties();

            foreach (PropertyInfo item in thisProperties)
                if (item.IsLocalizedProperty())
                    item.SetValue(this, item.GenerateKey(thisType, Id), null);
        }

        public virtual void GetKeys(TEntity ent)
        {
            Type thisType = GetType();
            Type entType = ent.GetType();
            PropertyInfo[] thisProperties = thisType.GetProperties();
            PropertyInfo[] entProperties = entType.GetProperties();

            foreach (PropertyInfo thisProperty in thisProperties)
            {
                if (thisProperty.IsLocalizedProperty())
                {
                    var entProperty = entProperties.FirstOrDefault(f => f.Name == thisProperty.Name);

                    if (entProperty == null)
                        continue;

                    thisProperty.SetValue(this, entProperty.GetValue(ent, null), null);
                }
            }
        }

        public virtual void AfterCreateEntity(TEntity t)
        {
        }

        public virtual void AfterUpdateEntity(TEntity t)
        {
        }

        public virtual void AfterDeleteEntity(TEntity t)
        {
        }
    }
}
