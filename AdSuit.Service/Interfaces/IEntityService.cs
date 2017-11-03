using AdSuit.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdSuit.Service.Interfaces
{

    public interface IEntityService<T> : IService
        where T : class
    {
        void Create(T entity);
        Task CreateAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        IEnumerable<T> GetAll();
        IQueryable<T> GetQueryableAll();
        void Update(T entity);
        Task UpdateAsync(T entity);
        T GetById(object EntityId);
        void Save();
        Task SaveAsync();
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetByIdAsync(object EntityId);
    }
}
