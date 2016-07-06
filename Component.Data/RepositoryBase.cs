using Component.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Component.Data
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {

        protected abstract IUnitWork EFContext
        {
            get; 
        }

        public IQueryable<TEntity> Entities
        {
            get
            {
                return EFContext.Set<TEntity,TKey>();
            }
        }

        public int Delete(IEnumerable<TEntity> entities)
        {
            return EFContext.RegisterDeleted<TEntity, TKey>(entities);
        }

        public int Delete(Expression<Func<TEntity, bool>> where)
        {
            return EFContext.RegisterDeleted(where);
        }

        public int Delete(TKey key)
        {
            TEntity entity = EFContext.Set<TEntity, TKey>().Find(key);
            return entity == null ? 0 : EFContext.RegisterDeleted<TEntity, TKey>(entity);
        }

        public int Delete(TEntity entity)
        {
            return EFContext.RegisterDeleted<TEntity, TKey>(entity);
        }

        public TEntity GetByKey(TKey key)
        {
            return EFContext.Set<TEntity, TKey>().Find(key);
        }

        public int Insert(IEnumerable<TEntity> entities)
        {
            return EFContext.RegisterNew<TEntity, TKey>(entities);
        }

        public int Insert(TEntity entity)
        {
            return EFContext.RegisterNew<TEntity, TKey>(entity);
        }

        public int Update(TEntity entity)
        {
            return EFContext.RegisterModified<TEntity, TKey>(entity);
        }

        public int Update(Dictionary<string, KeyValuePair<Type, string>> propertities, Expression<Func<TEntity, bool>> where)
        {
            return EFContext.RegisterModified(propertities, where);
        }
    }
}
