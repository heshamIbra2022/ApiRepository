using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingOrderController : ControllerBase
    {
        IShoppingCartRepository shoppingCartRepository;

        public ShoppingOrderController(IShoppingCartRepository shoppingCartRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
        }

        [HttpGet("getAllprdsOfOrders")]

        public IActionResult GetAll()
        {
            List<ShoppingOrderProducts> products = shoppingCartRepository.getAll();
            return Ok(products);
        }

        [HttpGet("getdetail/{id:int}")]
        public IActionResult getByID(int id)
        {
            ShoppingOrderProducts product = shoppingCartRepository.getById(id);
            if (product == null)
            {
                return BadRequest("Empty product");
            }
            return Ok(product);
        }

        [HttpGet("getprdbyOrder/{orderId:int}")]
        public IActionResult GetByOrder(int orderId)
        {
            List<ShoppingOrderProducts> products = shoppingCartRepository.getprdbyOrderId(orderId);
            return Ok(products);
        }



        [HttpPost]
        public IActionResult New (ShoppingOrderProducts shoppingproduct)
        {

            shoppingCartRepository.insert (shoppingproduct);

            return Ok(shoppingproduct);

        }


        [HttpDelete("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            ShoppingOrderProducts product = shoppingCartRepository.getById(id);
            if (product == null)
            {
                return BadRequest("Empty product");
            }
            else
            {
                shoppingCartRepository.delete(id);
                return Ok("deleted");
            }
           
        }
    }
}
