using E_Commerse2.Data;
using E_Commerse2.Models;
using E_Commerse2.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_Commerse2.Repository
{
    public class Repository<T>:IRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> dbSet;

        //ApplicationDbContext context = new ApplicationDbContext();
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet=context.Set<T>();
        }

        public List<T> GetAll(string? include = null)
        {
            if (include == null)
            {
                return dbSet.ToList();
            }
            else
            {
                return dbSet.Include(include).ToList();
            }
        }

        public T? GetAll(Expression<Func<T, bool>> expression)
        {
            return dbSet.Where(expression).FirstOrDefault();
        }

        public void Create(T intity)
        {
            dbSet.Add(intity);
        }

        public void Edit(T intity)
        {
            dbSet.Update(intity);
        }
        public void Delete(T intity)
        {
            dbSet.Remove(intity);
        }

        public void Commit()
        {
            context.SaveChanges();
        }
    }
}
