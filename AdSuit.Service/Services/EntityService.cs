using AdSuit.DAL;
using AdSuit.Repository.Interfaces;
using AdSuit.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdSuit.Service.Services
{
    public abstract class EntityService<T> : IEntityService<T> where T : class
    {
        IUnitOfWork _unitOfWork;
        IGenericRepository<T> _repository;

        public EntityService(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }


        public virtual void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Add(entity);
            _unitOfWork.Commit();
        }

        public virtual async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Add(entity);
            await _unitOfWork.CommitAsync();
        }


        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Update(entity);
            _unitOfWork.Commit();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public virtual T GetById(object EntityId) {
            if (EntityId == null) throw new ArgumentNullException("entity");
            return _repository.GetById(EntityId);
        }

        public virtual async Task<T> GetByIdAsync(object EntityId)
        {
            if (EntityId == null) throw new ArgumentNullException("entity");
            return await _repository.GetAsync((int)EntityId);
        }

        public virtual void Save()
        {
            _repository.Save();
            _unitOfWork.Commit();
        }

        public virtual async Task SaveAsync()
        {
            _repository.Save();
            await _unitOfWork.CommitAsync();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Delete(entity);
            _unitOfWork.Commit();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Delete(entity);
            await _unitOfWork.CommitAsync();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual IQueryable<T> GetQueryableAll()
        {
            return _repository.GetQueryableAll();
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
