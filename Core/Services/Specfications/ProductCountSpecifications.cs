using Domain.Models.Products;
using Shared.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specfications
{
    public class ProductCountSpecifications(ProductQueryPrams productQueryPrams)
        :BaseSpecifications<Product>(createcritera(productQueryPrams))
    {
        private static Expression<Func<Product, bool>> createcritera(ProductQueryPrams productQueryPrams)
        {

            return prod =>
          (!productQueryPrams.brandId.HasValue || prod.BrandId == productQueryPrams.brandId) &&
          (!productQueryPrams.TypeId.HasValue || prod.TypeId == productQueryPrams.TypeId) &&
          (string.IsNullOrWhiteSpace(productQueryPrams.Search)
          || prod.Name.ToLower().Contains(productQueryPrams.Search.ToLower()));
        }

    }
}
