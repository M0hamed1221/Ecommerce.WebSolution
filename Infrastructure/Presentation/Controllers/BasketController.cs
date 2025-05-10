using Microsoft.AspNetCore.Mvc;
using ServicesAbstracion;
using Shared.DTOs.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BasketController (IServiceManager _serviceManager): ControllerBase
    {
        //  1) Get User Basket
        [HttpGet]
        public async Task<ActionResult<BasketDto>> Get(string id)
        {
            var basket = await _serviceManager.basketService.GetAsync(id);
            return Ok(basket); 
        }

        // 2)Update User Basket
        //2.1)Create UserBasket
        //2.2)Add Item To UserBasket
        //2.3)Remove Item From UserBasket
        // 2.4) Update Basket Items Quantity

        [HttpPost]
        public async Task<ActionResult<BasketDto>> Update (BasketDto basketDto)
        {
           var basket= await _serviceManager.basketService.UpdateAsync(basketDto);
            return Ok(basket);
        }


        //3)Delete User Basket: => After Check Out

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete (string id)
        {
            await _serviceManager.basketService.DeleteAsync(id);
            return NoContent();
        }

    }
}
