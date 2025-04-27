using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.Controllers
{
    //baseURL+API/ControllerName/Segment[Get}

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("{id:int}")]
        public ActionResult<Product> GetById(int id)
        {
            return new Product() { Id = id }; 
        }

        /**/
        [HttpGet]
        public ActionResult<Product> GetAll()
        {
            return new Product() { Id = 5 };
        }
        [HttpPost]
        public ActionResult<Product> Add(Product product)
        {
            return new Product() { Id = product.Id,Name=product.Name };
        }
        [HttpPut]
        public ActionResult<Product> update(Product product)
        {
            return new Product() { Id = product.Id, Name = product.Name };
        }
        [HttpDelete]
        public ActionResult<Product> delete(Product product)
        {
            return new Product() { Id = product.Id, Name = product.Name };
        }
    } 
    public class Product()
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }


}
