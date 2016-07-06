using Component.Tools;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Component.Data
{
    public abstract class UnitWorkBase : IUnitWork,IDisposable 
    {
        protected abstract DbContext Context{ get; set; }
        private bool IsCommitted = true;
        private List<string> lSQL = new List<string>(); 
        public bool Truncation
        {
            get; set;
        }

        public int Commit()
        {
            if (IsCommitted)
                return 0;
            
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (var sql in lSQL)
                {
                    sb.Append(sb + ";");
                }
                int sqlcount = ExeSQL(sb.ToString());
                int modelcount = Context.SaveChanges();
                IsCommitted = true;
                return sqlcount + modelcount;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                {
                    SqlException sqlEx = e.InnerException.InnerException as SqlException;
                    string msg = DBHelper.ExceptionMessage(sqlEx.Number);
                    //throw PublicHelper.ThrowDataAccessException("提交数据更新时发生异常：" + msg, sqlEx);
                }
                //throw;

                return -1;
            }
        }
        public void Rollback()
        {
            IsCommitted = true;
            this.Dispose();
        }
        private int Save()
        {
            if (!Truncation)
            {
                return Commit();
            }
                
            return 0;
        }
        public void Dispose()
        {
            if (!IsCommitted)
            {
                Commit();
            }
            Context.Dispose();
        }

        public DbSet<TEntity> Set<TEntity,Tkey>() where TEntity:EntityBase<Tkey>
        {
            return Context.Set<TEntity>();
        }

        public int RegisterNew<TEntity,Tkey>(TEntity entity) where TEntity : EntityBase<Tkey>
        {
            EntityState state = Context.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                Context.Entry(entity).State = EntityState.Added;
            }
            IsCommitted = false;

            return Save();
        }

        public int RegisterNew<TEntity,Tkey>(IEnumerable<TEntity> entities) where TEntity : EntityBase<Tkey>
        {
            bool temptrun = Truncation;
            Truncation = true;
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                {
                    RegisterNew<TEntity,Tkey>(entity);
                }

            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
                Truncation = temptrun;
            }
            return Save();
        }

        public int RegisterModified<TEntity,Tkey>(TEntity entity) where TEntity : EntityBase<Tkey>
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Context.Set<TEntity>().Attach(entity);
            }
            Context.Entry(entity).State = EntityState.Modified;
            IsCommitted = false;
            
            return Save();
        }

        public int RegisterModified<TEntity>(Dictionary<string,KeyValuePair<Type,string>> propertities, Expression<Func<TEntity, bool>> where)
        {
            //if (Context.Entry(entity).State == EntityState.Detached)
            //{
            //    Context.Set<TEntity>().Attach(entity);
            //}

            ////获取到user的状态实体，可以修改其状态
            //var setEntry = ((IObjectContextAdapter)Context).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity);
            ////只修改实体的Name属性和Age属性
            //foreach(var pname in propertitynames)
            //{
            //    Context.Entry(TEntity).propertitynames(pname)
            //    setEntry.SetModifiedProperty(pname);
            //}

            //IsCommitted = false;
            //Context.Set<TEntity>().Where(where).
            //return Save();

            string sql = "update {0} set {1} where 1=1 {2}";
            string asname = "";
            string tablename = typeof(TEntity).Name;
            string wherestr = "";

            if (where != null && where.Parameters.Count > 0)
            {
                asname = where.Parameters[0].Name;
                tablename += " " + asname;
                wherestr = where.Body.ToString();
                asname += ".";
            }

            StringBuilder setstr = new StringBuilder();
            foreach(var p in propertities)
            {
                string val = p.Value.Value;
                if (p.Value.Key == typeof(string))
                    val = "'" + val + "'";

                setstr.Append(asname + p.Key + "=" + val + ",");
            }
            setstr.Remove(setstr.Length - 1, 1);
            sql = string.Format(sql, tablename, setstr, wherestr);
            lSQL.Add(sql);
            IsCommitted = false;
            return Save();

        }

        public int RegisterDeleted<TEntity,Tkey>(TEntity entity) where TEntity:EntityBase<Tkey>
        {
            Context.Entry(entity).State = EntityState.Deleted;
            IsCommitted = false;

            return Save();
        }

        public int RegisterDeleted<TEntity,Tkey>(IEnumerable<TEntity> entities) where TEntity : EntityBase<Tkey>
        {
            bool temptrun = Truncation;
            Truncation = true;

            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                {
                    RegisterDeleted<TEntity,Tkey>(entity);
                }
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
                Truncation = temptrun;
            }
            return Save();
        }

        public int RegisterDeleted<TEntity>(Expression<Func<TEntity, bool>> where)
        {
            if(where != null && where.Parameters.Count>0)
            {
                string sql = "delete from {0} where {1}";
                string tablename = typeof(TEntity).Name + " " + where.Parameters[0].Name;
                string wherestr = where.Body.ToString();

                sql = string.Format(sql, tablename, wherestr);

                lSQL.Add(sql);
                IsCommitted = false;
                return Save();

            }
            return 0;
        }

        public int ExeSQL(string sql)
        {
            return Context.Database.ExecuteSqlCommand(sql);
        }

        public IEnumerable<TEntity> QuerySQL<TEntity>(string sql)
        {
            return Context.Database.SqlQuery<TEntity>(sql).ToList<TEntity>();
        }

    }
}
