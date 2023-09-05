using Blog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        T? GetById(long id);
        T? GetByCondition(Func<T, bool> expr);
        IList<T> GetAll(int page = 1, int size = 10);
        IList<T> SearchByCondition(Func<T, bool> expr, int page = 1, int size = 10);
        void Insert(T obj);
        void Update(T obj);
    }

    public abstract class GenericRepository<T> where T : BaseEntity
    {
        private readonly BlogContext context;
        private DbSet<T> dbSet;


        public GenericRepository(BlogContext context)
        {
            dbSet = context.Set<T>();
            this.context = context;
        }

        public T? GetById(long id)
        {
            return dbSet.FirstOrDefault(entity => entity.Id == id);
        }

        public T? GetByCondition(Func<T, bool> expr)
        {
            return dbSet.Where(expr).FirstOrDefault();
        }

        public IList<T> GetAll(int page = 1, int size = 10)
        {
            return dbSet.Skip((page - 1) * size).Take(size).ToList();
        }

        public IList<T> SearchByCondition(Func<T, bool> expr, int page = 1, int size = 10)
        {
            return dbSet.Where(expr).Skip((page - 1) * size).Take(size).ToList();
        }

        public void Insert(T obj)
        {
            dbSet.Add(obj);
            context.SaveChanges();
        }

        public void Update(T obj)
        {
            context.SaveChanges();
        }
    }
}
