using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portal.Apis.Core.Configuration;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Core.Extensions;

namespace Portal.Apis.Core.BLL
{
    public class AuditableEntityService<TKey, TEntity> : EntityService<TEntity>
        where TEntity : class, IAuditableEntity<TKey>
    {
        private static Expression<Func<TEntity, bool>> _checkForIsNotDeleted;

        public AuditableEntityService(IEntityRepository<TEntity> repository) : base(repository)
        {
        }

        #region Properties
        private static Expression<Func<TEntity, bool>> CheckForIsNotDeleted
        {
            get
            {
                if (_checkForIsNotDeleted == null)
                {
                    var ent = Expression.Parameter(typeof(TEntity), "ent");
                    var prop = Expression.Property(ent, Settings.IS_DELETED_PROPERTY);
                    var value = Expression.Constant(false);

                    _checkForIsNotDeleted = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(prop, value), ent);
                }
                return _checkForIsNotDeleted;
            }
        }
        #endregion

        public override IQueryable<TEntity> AllAsQueryable
        {
            get
            {
                return base.AllAsQueryable.Where(CheckForIsNotDeleted);
            }
        }
    }
}