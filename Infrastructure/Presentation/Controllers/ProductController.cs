using Microsoft.AspNetCore.Mvc;
using ServicesAbstracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Shared.DTOs.Products;
using Shared.Enums;
using Shared;
using Microsoft.AspNetCore.Authorization;
namespace Presentation.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class ProductController(IServiceManager _serviceManager ):ControllerBase
    {
        //Get All Products
        [HttpGet] //Get
        public async Task<ActionResult<PaginatedResponse<ProductResponse>>> GetAllProducts([FromQuery]ProductQueryPrams productQueryPrams )
        {

            var products =await _serviceManager.prodectService.GetAllProductAsync(productQueryPrams);
            return Ok(products);
        }

        //Get  Product by id
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ProductResponse>GetProductByID(int id)
        {
            var product =await  _serviceManager.prodectService.GetProductByIDAsync(id);
            return product;
        }

        //Get All ProductBrands
        [HttpGet("brands")]
        public async Task<ActionResult<BrandResponse>>GetAllBrands()
        {
            var brands = await _serviceManager.prodectService.GetAllBrandsAsync();
            return Ok(brands);
        }


        //Get All ProductTypes

        [HttpGet("types")]
        public async Task<ActionResult<TypeResponse>> GetAllTypes()
        {
            var types = await _serviceManager.prodectService.GetAllTypeAsync();
            return Ok(types);
        }



        //add  Product
        //update  Product
        //delete  Product





    }
}
