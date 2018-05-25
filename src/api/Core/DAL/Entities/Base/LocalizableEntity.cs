using System;
using System.Collections.Generic;

namespace Portal.Apis.Core.DAL.Entities
{
    public interface ILocalizable<TEntity, TEntity_Locale>
        where TEntity : class
        where TEntity_Locale : ILocale<TEntity>
    {
        IList<TEntity_Locale> Localizations { get; set; }
    }

    public interface ILocale<TEntity>
        where TEntity : class
    {
        int CultureId { get; set; }
        int LocalizableEntityId { get; set; }

        Culture Culture { get; set; }
        TEntity LocalizableEntity { get; set; }
    }

    public class LocalizableEntity<TEntity, TEntityLocale> : BaseEntity, ILocalizable<TEntity, TEntityLocale>
        where TEntity : class
        where TEntityLocale : LocalizableEntity_Locale<TEntity>
    {
        public LocalizableEntity()
        {
            Localizations = new List<TEntityLocale>();
        }

        public virtual IList<TEntityLocale> Localizations { get; set; }
    }

    public class LocalizableEntity_Locale<TEntity> : BaseEntity, ILocale<TEntity>
        where TEntity : class
    {
        public int CultureId { get; set; }
        public int LocalizableEntityId { get; set; }

        public virtual Culture Culture { get; set; }
        public virtual TEntity LocalizableEntity { get; set; }
    }
}
