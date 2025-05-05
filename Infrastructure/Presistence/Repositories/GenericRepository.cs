using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext _storeDbContext) :
        IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public void Add(TEntity entity)
        {
            _storeDbContext.Set<TEntity>().Add(entity);
        }
        public void Update(TEntity entity)
        {
            _storeDbContext.Set<TEntity>().Update(entity);

        }
        public void Delete(TEntity entity)
        {
            _storeDbContext.Set<TEntity>().Remove(entity);

        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
          return await  _storeDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _storeDbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(ISpecfications<TEntity> specfications)
        {
            var res =await SpecificationsEvaluator
                   .CreateQueary(_storeDbContext.Set<TEntity>(), specfications)
                   .FirstOrDefaultAsync();
            return res;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecfications<TEntity> specfications)
        {
            var res = await SpecificationsEvaluator
       .CreateQueary(_storeDbContext.Set<TEntity>(), specfications)
       .ToListAsync();
            return res;
        }
    }
}
