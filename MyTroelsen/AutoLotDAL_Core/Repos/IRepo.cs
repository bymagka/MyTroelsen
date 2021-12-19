using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;


namespace AutoLotDAL_Core.Repos
{
    public interface IRepo<T>
    {
        int Add(T entity);

        int Add(IList<T> entities);

        int Update(T entity);

        int Update(IList<T> entities);

        int Delete(int Id, byte[] timeStamp);

        int Delete(T entity);

        T GetOne(int? id);

        List<T> GetAll();

        List<T> GetSome(Expression<Func<T, bool>> where);

        List<T> GetAll<TSortField>(Expression<Func<T,TSortField>> orderBy,bool ascending);

        List<T> ExecuteQuery(string sql);

        List<T> ExecuteQuery(string sql,object[] sqlParameterObjects);
    }
}
