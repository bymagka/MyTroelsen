using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using AutoLotDAL_Core.EF;
using AutoLotDAL_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AutoLotDAL_Core.Repos
{
    public class BaseRepo<T> : IDisposable, IRepo<T> where T:EntityBase,new()
    {
        private readonly DbSet<T> _table;
        private readonly AutoLotEntities _db;

        protected AutoLotEntities context => _db;

        public BaseRepo() : this(new AutoLotEntities())
        {
  
        }

        public BaseRepo(AutoLotEntities context)
        {
            _db = context;
            _table = _db.Set<T>();
        }

        public int Add(T entity)
        {
            _table.Add(entity);
            return SaveChanges();
        }

        public int Add(IList<T> entities)
        {
            _table.AddRange(entities);
            return SaveChanges();
        }

        public int Update(T entity)
        {
            _table.Update(entity);
            return SaveChanges();
        }

        public int Update(IList<T> entity)
        {
            _table.UpdateRange(entity);
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
            return _table.FromSqlRaw(sql).ToList();
        }

        public List<T> ExecuteQuery(string sql, object[] sqlParameterObjects)
        {
            return _table.FromSqlRaw(sql, sqlParameterObjects).ToList();
        }

        public virtual List<T> GetAll()
        {
            return _table.ToList();
        }

        public List<T> GetAll<TSortField>(Expression<Func<T, TSortField>> orderBy, bool ascending) => (ascending ? _table.OrderBy(orderBy) : _table.OrderByDescending(orderBy)).ToList();

        public List<T> GetSome(Expression<Func<T, bool>> where) => _table.Where(where).ToList();

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
            catch(RetryLimitExceededException ex)
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
