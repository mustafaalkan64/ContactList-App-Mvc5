using AdSuit.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdSuit.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        IEnumerable<T> GetAll();
        IQueryable<T> GetQueryableAll();
        Task<ICollection<T>> GetAllAsync();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T GetById(object EntityId);
        //T Delete(T entity);
        void Update(T entity);
        void Save();
        void Delete(object EntityId);
        void Delete(T Entity);
        T Get(int id);
        Task<T> GetAsync(int id);
    }
}
