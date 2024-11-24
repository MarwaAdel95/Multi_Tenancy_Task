using System.Linq.Expressions;
using Multi_Tenancy_Task.Entities;

namespace Multi_Tenancy_Task.Repositories
{
    public interface IBaseRepository <T> 
    {
        T Insert(T entity);
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression< Func<T,bool>> predicate);
        void SaveChanges();
        bool Any(Expression<Func<T, bool>> predicate);
    }
}
