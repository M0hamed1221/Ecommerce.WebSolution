using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specfications
{
    internal abstract class BaseSpecifications<T> : ISpecfications<T> where T : class
    {
        public BaseSpecifications(Expression<Func<T, bool>> _critera)
        {
            Critera = _critera;
        }
        public Expression<Func<T, bool>> Critera{ get; private set; }

        public List<Expression<Func<T, object>>> IncludeExpression { get; } = [];

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

      
        protected void AddInclude(Expression<Func<T, object>> include)
        {
            IncludeExpression.Add(include);
        }
        protected void AddOrderBy(Expression<Func<T, object>> order)
        {
            OrderBy = order;
        }
        protected void AddOrderByDescending(Expression<Func<T, object>> order)
        {
            OrderByDescending = order;
        }
        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPaginated { get; private set; }
        protected void ApplyPagination(int PageSize,int PageIndex)
        {
            IsPaginated = true;
            Take = PageSize;
               Skip = (PageIndex - 1) * PageSize;
        }

    }
}
