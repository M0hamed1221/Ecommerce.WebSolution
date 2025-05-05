using Shared;
using Shared.DTOs.Products;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstracion
{
   public interface  IProdectService
    {
        //Get All Products

        Task <PaginatedResponse<ProductResponse>> GetAllProductAsync(ProductQueryPrams productQueryPrams);
        //Get  Product by id
        Task<ProductResponse> GetProductByIDAsync(int id);
        //Get All ProductBrands
        Task<IEnumerable<BrandResponse>> GetAllBrandsAsync();

        //Get All ProductTypes
        Task<IEnumerable<TypeResponse>> GetAllTypeAsync();

    }
}
