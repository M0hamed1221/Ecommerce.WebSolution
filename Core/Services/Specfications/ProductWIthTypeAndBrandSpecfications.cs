using Domain.Models;
using Shared.DTOs.Products;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specfications
{
    class ProductWIthTypeAndBrandSpecfications : BaseSpecifications<Product>
    {
        //To Get Product By ID
        public ProductWIthTypeAndBrandSpecfications(int id) : base(prod=>prod.Id==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

        }
        //To Get all Product 
        public ProductWIthTypeAndBrandSpecfications(ProductQueryPrams productQueryPrams) : base(createcritera(productQueryPrams))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            ApplySorting(productQueryPrams);
            ApplyPagination(productQueryPrams.PageSize, productQueryPrams.PageIndex);

        }

        private static Expression<Func<Product,bool>> createcritera(ProductQueryPrams productQueryPrams)
        {

            return prod =>
          (!productQueryPrams.brandId.HasValue || prod.BrandId == productQueryPrams.brandId) &&
          (!productQueryPrams.TypeId.HasValue || prod.TypeId == productQueryPrams.TypeId) &&
          (string.IsNullOrWhiteSpace(productQueryPrams.Search)
          || prod.Name.ToLower().Contains(productQueryPrams.Search.ToLower()));
        }
        private  void ApplySorting(ProductQueryPrams productQueryPrams)
        {
            switch (productQueryPrams.productSortingOptions)
            {
                case ProductSortingOptions.nameAsc:
                    AddOrderBy(prod => prod.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(prod => prod.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(prod => prod.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(prod => prod.Price);
                    break;
            }
        }
    }
}
