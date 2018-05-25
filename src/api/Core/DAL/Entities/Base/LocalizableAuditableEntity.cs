using System;
using System.Collections.Generic;

namespace Portal.Apis.Core.DAL.Entities
{
    public class LocalizableAuditableEntity<TEntity, TEntityLocale> : AuditableBaseEntity, ILocalizable<TEntity, TEntityLocale>
        where TEntity : class
        where TEntityLocale : LocalizableEntity_Locale<TEntity>
    {
        public LocalizableAuditableEntity()
        {
            Localizations = new List<TEntityLocale>();
        }

        public virtual IList<TEntityLocale> Localizations { get; set; }
    }
}
