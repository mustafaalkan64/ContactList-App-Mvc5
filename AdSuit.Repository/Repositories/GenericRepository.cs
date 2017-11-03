using AdSuit.DAL;
using AdSuit.DAL.Interfaces;
using AdSuit.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdSuit.Repository.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
      where T : class
    {
        protected DbContext _entities;
        protected readonly IDbSet<T> _dbset;

        public GenericRepository(DbContext context)
        {
            _entities = context;
            _dbset = context.Set<T>();
        }

        public virtual T GetById(object EntityId)
        {
            T entity = _dbset.Find(EntityId);
            return entity;
        }

        public T Get(int id)
        {
            return _entities.Set<T>().Find(id);
        }

        public async Task<T> GetAsync(int id)
        {
            return await _entities.Set<T>().FindAsync(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbset.AsEnumerable<T>();
        }

        public virtual IQueryable<T> GetQueryableAll()
        {
            return _dbset.AsQueryable<T>();
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _entities.Set<T>().ToListAsync();
        }

        public IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            IEnumerable<T> query = _dbset.AsNoTracking().Where(predicate).AsEnumerable();
            return query;
        }

        public virtual T Add(T entity)
        {
            return _dbset.Add(entity);
        }

        //public virtual T Delete(T entity)
        //{
        //    return _dbset.Remove(entity);
        //}

        public virtual void Delete(object EntityId)
        {
            T entityToDelete = _dbset.Find(EntityId);
            Delete(entityToDelete);
        }
        public virtual void Delete(T Entity)
        {
            if (_entities.Entry(Entity).State == EntityState.Detached) //Concurrency için 
            {
                _dbset.Attach(Entity);
            }
            _dbset.Remove(Entity);
        }

        //public virtual void Edit(T entity)
        //{
        //    _entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        //    _entities.SaveChanges();
        //}

        public virtual void Update(T entity)
        {
            _dbset.Attach(entity);
            var entry = _entities.Entry(entity);
            entry.State = EntityState.Modified;

        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }
    }
}
