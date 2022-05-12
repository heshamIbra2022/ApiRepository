using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.DTO;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOrderController : ControllerBase
    {
        IUserOrderRepository UserOrder;

        public UserOrderController(IUserOrderRepository userOrder)
        {
            UserOrder = userOrder;
        }
        [HttpGet("getAllOrders")]
        public IActionResult GetAll()
        {
            List<UserOrder> orders = UserOrder.getAll();
            return Ok(orders);
        }
        [HttpPost("addorder")]
        public IActionResult New(userOrderDTO  userOrderDTO)
        {
            UserOrder userOrder = new UserOrder();
            userOrder.ApplicationUserId = userOrderDTO.ApplicationUserId;
            
            UserOrder.insert(userOrder);
            UserOrder order;

            order = UserOrder.getByUserId(userOrder.ApplicationUserId);
            return Ok(order);
        }

        [HttpGet("getdetail/{id:int}")]
        public IActionResult getByID(int id)
        {
            UserOrder order = UserOrder.getById(id);
            if (order == null)
            {
                return BadRequest("Empty order");
            }
            return Ok(order);
        }

        [HttpGet("getdetail/{username:alpha}")]
        public  IActionResult getByuserID(string username)
        {
            UserOrder order =  UserOrder.getByUserId(username);
            if (order == null)
            {
                return BadRequest("Empty order");
            }
            return Ok(order);
        }

    }
}
