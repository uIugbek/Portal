using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portal.Apis.Core.DAL.Entities;

namespace Portal.Apis.Core.BLL
{
    public class EntityService<TEntity> : IEntityService<TEntity>
        where TEntity : class
    {
        private readonly IEntityRepository<TEntity> _repository;

        public EntityService(IEntityRepository<TEntity> repository)
        {
            this._repository = repository;
        }

        #region Properties
        public virtual IQueryable<TEntity> AllAsQueryable
        {
            get { return _repository.DbSet.AsQueryable().AsNoTracking(); }
        }

        public virtual TEntity[] All
        {
            get { return AllAsQueryable.ToArray(); }
        }

        public virtual int Count
        {
            get { return AllAsQueryable.Count(); }
        }

        #endregion

        #region Methods
        public virtual TEntity ByID(object id)
        {
            return _repository.DbSet.Find(id);
        }

        public virtual DbSet<TEntity> AsObjectQuery()
        {
            return _repository.DbSet;
        }

        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return AllAsQueryable.Where(predicate);
        }

        public virtual IQueryable<TEntity> Filter(
           Expression<Func<TEntity, bool>> filter,
           Func<IQueryable<TEntity>,
               IOrderedQueryable<TEntity>> orderBy,
           out int total,
           int index = 0,
           int size = 25)
        {
            int skipCount = index * size;
            IQueryable<TEntity> resetSet =
                orderBy(filter != null ? AllAsQueryable.Where(filter) : AllAsQueryable);
            total = resetSet.Count();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            return resetSet;
        }
        public virtual bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.DbSet.Count(predicate) > 0;
        }

        public virtual TEntity Create(TEntity entity, bool autoSave = true)
        {
            return _repository.Create(entity, autoSave);
        }

        public virtual bool TryCreate(ref TEntity entity)
        {
            try
            {
                entity = _repository.Create(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual TEntity Update(TEntity entity, bool autoSave = true)
        {
            return _repository.Update(entity, autoSave);
        }

        public virtual bool TryUpdate(ref TEntity entity)
        {
            try
            {
                entity = _repository.Update(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual void Delete(object id,
                                bool directly = false,
                                bool autoSave = true,
                                Type[] notIncludedEntityTypes = null)
        {
            TEntity entityToDelete = ByID(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entity,
                                   bool directly = false,
                                   bool autoSave = true,
                                   Type[] notIncludedEntityTypes = null)
        {
            _repository.Delete(entity, directly, autoSave, notIncludedEntityTypes);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate,
                                 bool directly = false,
                                 bool autoSave = true,
                                 Type[] notIncludedEntityTypes = null)
        {
            IQueryable<TEntity> entitiesToDelete = Filter(predicate);

            foreach (TEntity entity in entitiesToDelete)
                this.Delete(entity, directly, false, notIncludedEntityTypes);

            if (autoSave)
                Save();
        }

        public virtual void Delete(IEnumerable<TEntity> entitiesToDelete,
                                bool directly = false,
                                bool autoSave = true,
                                Type[] notIncludedEntityTypes = null)
        {
            foreach (TEntity entity in entitiesToDelete)
                this.Delete(entity, directly, false, notIncludedEntityTypes);

            if (autoSave)
                Save();
        }

        public virtual bool TryDelete(TEntity entity,
                                       bool directly = false,
                                       bool autoSave = true,
                                       Type[] notIncludedEntityTypes = null)
        {
            try
            {
                this.Delete(entity, directly, autoSave, notIncludedEntityTypes);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual void ChangeEntityState(TEntity entity, EntityState state)
        {
            _repository.DbContext.Entry<TEntity>(entity).State = state;
        }

        public virtual void Save()
        {
            _repository.DbContext.SaveChanges();
        }
        #endregion
    }

    public interface IEntityService<TEntity>
        where TEntity : class
    {
        TEntity[] All { get; }

        IQueryable<TEntity> AllAsQueryable { get; }
        int Count { get; }

        TEntity ByID(object id);

        bool Contains(Expression<Func<TEntity, bool>> predicate);

        DbSet<TEntity> AsObjectQuery();

        TEntity Create(TEntity entity, bool autoSave = true);

        void Delete(Expression<Func<TEntity, bool>> predicate,
                    bool directly = false,
                    bool autoSave = true,
                    Type[] notIncludedEntityTypes = null);

        void Delete(IEnumerable<TEntity> entitiesToDelete,
                    bool directly = false,
                    bool autoSave = true,
                    Type[] notIncludedEntityTypes = null);

        void Delete(object id,
                    bool directly = false,
                    bool autoSave = true,
                    Type[] notIncludedEntityTypes = null);

        /// <summary>
        /// Delete from database
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="directly">If false, will be set IsDeleted = true, else will be delete directly.</param>
        /// <param name="autoSave">If true, changes will save automatically.</param>
        /// <param name="notIncludedEntityTypes">Types not included on deleting.</param>
        void Delete(TEntity entity,
                    bool directly = false,
                    bool autoSave = true,
                    Type[] notIncludedEntityTypes = null);

        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter,
                                   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, out int total,
                                   int index = 0, int size = 25);

        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

        bool TryCreate(ref TEntity entity);

        bool TryDelete(TEntity entity,
                       bool directly = false,
                       bool autoSave = true,
                       Type[] notIncludedEntityTypes = null);

        bool TryUpdate(ref TEntity entity);

        TEntity Update(TEntity entity, bool autoSave = true);

        void ChangeEntityState(TEntity entity, EntityState state);

        void Save();
    }
}