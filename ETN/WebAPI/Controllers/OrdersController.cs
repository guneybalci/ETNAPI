using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Create an Order
        /// If you are not authorized, you cannot perform this action. Create user or sign in
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost("add")]
        //[Authorize]
        public IActionResult Add(List<Order> orders)
        {
            var result = _orderService.Add(orders);
            if (result.Success)
            {
                //return Ok(result.Data.Select(x=> new Order
                //{
                //    Id = x.Id,
                //    OrderNo= x.OrderNo,
                //    Status = x.Status
                //}));

                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update the Order
        /// If you are not authorized, you cannot perform this action. Create user or sign in
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost("update")]
        //[Authorize]
        public IActionResult Update(Order order)
        {
            var result = _orderService.Update(order);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete the Order
        /// If you are not authorized, you cannot perform this action. Create user or sign in
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>

        [HttpPost("delete")]
        //[Authorize]
        public IActionResult Delete(Order order)
        {
            var result = _orderService.Delete(order);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        /// <summary>
        ///  Get All Orders
        ///  If you are not authorized, you cannot perform this action. Create user or sign in
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        //[Authorize]
        public IActionResult GetAll()
        {
            //Thread.Sleep(1000);
            var result = _orderService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _orderService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyproduct")]
        public IActionResult GetByProduct(int productCode)
        {
            var result = _orderService.GetByProductCode(productCode);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
