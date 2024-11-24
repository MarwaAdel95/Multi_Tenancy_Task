using System.Linq.Expressions;
using Multi_Tenancy_Task.Data;
using Multi_Tenancy_Task.Entities;

namespace Multi_Tenancy_Task.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Any(predicate);   
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {

            return _context.Set<T>();
        }

        public T Insert(T entity)
        {
            _context.Add(entity);
            return entity;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
}
    }

