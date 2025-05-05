using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
   public interface ISpecfications<T>where T:class
    {
        Expression<Func<T, Object>> OrderBy { get; }/*For Order*/
        Expression<Func<T, Object>> OrderByDescending { get; }

        // _storeDbContext.set(T).Where(Expression<Func<T,bool>>)
        Expression<Func<T,bool>> Critera { get; }
        // _storeDbContext.set(T).select(Expression<Func<T,object>>)

        //Iclude
        List<Expression<Func<T, object>>>  IncludeExpression { get; }

        int Skip { get;  }
        int Take { get; }

        bool IsPaginated { get; }

    }
}
