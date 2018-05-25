using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Portal.Apis.Core.Configuration;
using Portal.Apis.Core.Extensions;

namespace Portal.Apis.Core.BLL
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity> 
        where TEntity : class
    {
        private static object DbLock = new object();
        public EntityRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        #region Properties
        public DbContext DbContext { get; }
        protected readonly DbSet<TEntity> _dbSet;
        public DbSet<TEntity> DbSet
        {
            get
            {
                lock (DbLock)
                {
                    return _dbSet;
                }
            }
        }
        #endregion

        #region Methods
        public TEntity Create(TEntity entity, bool autoSave)
        {
            _dbSet.Add(entity);

            DbContext.Entry(entity).State = EntityState.Added;

            if (autoSave)
            {
                lock (DbLock)
                {
                    DbContext.SaveChanges();
                }
            }
            return entity;
        }

        public TEntity Update(TEntity entity, bool autoSave)
        {
            // _dbSet.Attach(entity);
            DbSet.Update(entity);

            DbContext.Entry(entity).State = EntityState.Modified;

            if (autoSave)
            {
                lock (DbLock)
                {
                    DbContext.SaveChanges();
                }
            }
            return entity;
        }

        public void Delete(TEntity entity, bool directly, bool autoSave, Type[] notIncludedEntityTypes)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            var typeOfTEntity = typeof(TEntity);
            if (!directly && typeOfTEntity.GetProperties().Any(a => a.Name.Equals(Settings.IS_DELETED_PROPERTY)))
            {
                entity.MarkAsDeleted(typeOfEntity: typeOfTEntity, notInculedEntityTypes: notIncludedEntityTypes);
            }
            else
            {
                _dbSet.Remove(entity);
                DbContext.Entry(entity).State = EntityState.Deleted;
            }

            if (autoSave)
            {
                lock (DbLock)
                {
                    DbContext.SaveChanges();
                }
            }
        }
        #endregion

    }
    public interface IEntityRepository<TEntity> where TEntity : class
    {
        DbContext DbContext { get; }
        DbSet<TEntity> DbSet { get; }

        TEntity Create(TEntity entity, bool autoSave = true);

        TEntity Update(TEntity entity, bool autoSave = true);

        void Delete(TEntity entity, bool directly = false, bool autoSave = true, Type[] notIncludedEntityTypes = null);
    }


}