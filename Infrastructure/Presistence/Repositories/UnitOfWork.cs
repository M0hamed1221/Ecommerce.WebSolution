using Domain.Contracts;
using Domain.Models;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class UnitOfWork(StoreDbContext _storeDbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositiors = [];

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (_repositiors.ContainsKey(typeName))
                return (GenericRepository<TEntity, TKey>)_repositiors[typeName];
            else
            {
                var repo= new GenericRepository<TEntity, TKey>(_storeDbContext);
                _repositiors[typeName] = repo;
                return repo;
            }
                 
        }

        public  async Task<int> SaveChanges()
        {
          return await  _storeDbContext.SaveChangesAsync();

        }
    }
}
