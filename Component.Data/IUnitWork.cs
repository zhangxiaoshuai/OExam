using Component.Tools;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Component.Data
{
    public interface IUnitWork 
    {
        bool Truncation { get; set; }
        int Commit();
        void Rollback();

        /// <summary>
        ///   为指定的类型返回 System.Data.Entity.DbSet，这将允许对上下文中的给定实体执行 CRUD 操作。
        /// </summary>
        /// <typeparam name="TEntity"> 应为其返回一个集的实体类型。 </typeparam>
        /// <returns> 给定实体类型的 System.Data.Entity.DbSet 实例。 </returns>
        DbSet<TEntity> Set<TEntity,Tkey>() where TEntity:EntityBase<Tkey>;

        /// <summary>
        ///   注册一个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        int RegisterNew<TEntity,Tkey>(TEntity entity) where TEntity : EntityBase<Tkey>;

        /// <summary>
        ///   批量注册多个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        int RegisterNew<TEntity,Tkey>(IEnumerable<TEntity> entities) where TEntity : EntityBase<Tkey>;

        /// <summary>
        ///   注册一个更改的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        int RegisterModified<TEntity,Tkey>(TEntity entity) where TEntity : EntityBase<Tkey>;
        /// <summary>
        /// 按条件更改对象到仓储上下文中
        /// </summary>
        /// <param name="entity">要变更的属性对象</param>
        /// <param name="propertitynames">要变更的属性名</param>
        /// <param name="where">变更条件</param>
        int RegisterModified<TEntity>(Dictionary<string, KeyValuePair<Type, string>> propertities, Expression<Func<TEntity, bool>> where);
        /// <summary>
        ///   注册一个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        int RegisterDeleted<TEntity,Tkey>(TEntity entity) where TEntity : EntityBase<Tkey>;

        /// <summary>
        ///   批量注册多个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        int RegisterDeleted<TEntity,Tkey>(IEnumerable<TEntity> entities) where TEntity : EntityBase<Tkey>;
        /// <summary>
        /// 按条件删除对象到仓储上下文中
        /// </summary>
        /// <param name="where">条件</param>
        int RegisterDeleted<TEntity>(Expression<Func<TEntity, bool>> where);

        int ExeSQL(string sql);
        IEnumerable<TEntity> QuerySQL<TEntity>(string sql);
    }
}
