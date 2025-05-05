using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Specfications;
using ServicesAbstracion;
using Shared;
using Shared.DTOs.Products;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _imapper) : IProdectService
    {
        public  async Task<IEnumerable<BrandResponse>> GetAllBrandsAsync()
        {
            var repos = _unitOfWork.GetRepository<ProductBrand, int>();
            var brands = await repos.GetAllAsync();
            var result = _imapper.Map<IEnumerable<BrandResponse>>(brands);
            return    result;
        }

        public async Task<PaginatedResponse<ProductResponse>> GetAllProductAsync(ProductQueryPrams productQueryPrams)
        {
            var specs = new ProductWIthTypeAndBrandSpecfications( productQueryPrams);//no filter
            var product = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(specs);

            var ProductresPonse= _imapper.Map<IEnumerable<ProductResponse>>(product);
            var result = new PaginatedResponse<ProductResponse>()
            {
                Data = ProductresPonse,
                PageSize = productQueryPrams.PageSize,
                PageIndex = productQueryPrams.PageIndex,
                TotalCount = ProductresPonse.Count()
            };
            return result;
        }

        public async Task<IEnumerable<TypeResponse>> GetAllTypeAsync()
        {
            var repos = _unitOfWork.GetRepository<ProductType, int>();
            var type = await repos.GetAllAsync();
            var result = _imapper.Map<IEnumerable<TypeResponse>>(type);
            return result;
        }

        public async Task<ProductResponse> GetProductByIDAsync(int id)
        {
            var specs = new ProductWIthTypeAndBrandSpecfications(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specs);
            return _imapper.Map<ProductResponse>(product);
        }
    }
}
