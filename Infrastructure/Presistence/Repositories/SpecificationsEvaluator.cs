using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
   public  static class SpecificationsEvaluator
    {
        public static IQueryable<T> CreateQueary<T>(IQueryable<T> inputQuery, ISpecfications<T> specfications) where T : class
        {
            var Query = inputQuery;
            if(specfications.Critera is not null)/* Filter */
            {
                Query.Where(specfications.Critera);
            }
            //foreach (var include in specfications.IncludeExpression)
            //{
            //    Query.Include(include);/*p=>p.ProductBrand*/
            //}
           
            if (specfications.OrderBy is not null)/* Filter */
            {
                Query.OrderBy(specfications.OrderBy);
            }
            if (specfications.OrderByDescending is not null)/* Filter */
            {
                Query.OrderBy(specfications.OrderByDescending );
            }
            if (specfications.IsPaginated)
                Query = Query.Skip(specfications.Skip).Take(specfications.Take);
            Query = specfications.IncludeExpression.
              Aggregate(Query, (currentQuery, include)
               => currentQuery.Include(include));
            return Query;

        }
    }
}
