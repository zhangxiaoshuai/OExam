using Component.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Component.Data
{
    public interface IRepository<TEntity,TKey> where TEntity:EntityBase<TKey>
    {
        IQueryable<TEntity> Entities { get; }
        int Insert(TEntity entity);
        int Insert(IEnumerable<TEntity> entities);
        int Delete(TEntity entity);
        int Delete(IEnumerable<TEntity> entities);
        int Delete(TKey key);
        int Delete(Expression<Func<TEntity, bool>> where);
        int Update(TEntity entity);
        int Update(Dictionary<string, KeyValuePair<Type, string>> propertities, Expression<Func<TEntity, bool>> where);
        TEntity GetByKey(TKey key);

    }
}
