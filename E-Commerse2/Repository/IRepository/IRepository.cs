using E_Commerse2.Models;
using System.Linq.Expressions;

namespace E_Commerse2.Repository.IRepository
{
    public interface IRepository<T>where T : class
    {
        List<T> GetAll(string? include = null);


        T? GetAll(Expression<Func<T,bool>> expression);


        void Create(T intity);


        void Edit(T intity);

        void Delete(T intity);


        void Commit();
    }
}
