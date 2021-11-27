using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using AutoLotDAL.EF;
using AutoLotDAL.Models.Base;

namespace AutoLotDAL.Repos
{
    public class BaseRepo<T> : IDisposable, IRepo<T> where T:EntityBase,new()
    {
        private readonly DbSet<T> _table;
        private readonly AutoLotEntities _db;

        protected AutoLotEntities context => _db;

        public BaseRepo()
        {
            _db = new AutoLotEntities();
            _table = _db.Set<T>();
        }
        public int Add(T entity)
        {
            _table.Add(entity);
            return SaveChanges();
        }

        public int AddRange(IList<T> entities)
        {
            _table.AddRange(entities);
            return SaveChanges();
        }

        public int Delete(int carId, byte[] timeStamp)
        {
            _db.Entry(new T() { Id = carId, Timestamp = timeStamp }).State = EntityState.Deleted;
            return SaveChanges();
        }

        public int Delete(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }

        public List<T> ExecuteQuery(string sql)
        {
            return _table.SqlQuery(sql).ToList();
        }

        public List<T> ExecuteQuery(string sql, object[] sqlParameterObjects)
        {
            return _table.SqlQuery(sql,sqlParameterObjects).ToList();
        }

        public virtual List<T> GetAll()
        {
            return _table.ToList();
        }

        public T GetOne(int? id)
        {
            return _table.Find(id);
        }

        public int Save(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }

        internal int SaveChanges()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch(DbUpdateException ex)
            {
                throw;
            }
            catch(CommitFailedException ex)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
