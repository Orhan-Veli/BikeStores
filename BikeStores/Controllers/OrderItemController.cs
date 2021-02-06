using BikeStores.Data.Entities;
using BikeStores.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStores.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        BikeStoresContext _context;
        public OrderItemController()
        {
            _context = new BikeStoresContext();
        }
        [HttpGet()]
        public ActionResult<List<OrderItem>> GetOrIt()
        {
            var entity = _context.OrderItems.Where(x=> !x.IsDeleted);
            if (entity==null)
            {
                return NoContent();
            }
            var entityDto = entity.Select(x => new OrderItemListDto 
            {
                ItemId=x.ItemId,
                OrderId=x.OrderId,
                ProductId=x.ProductId
            });
            return Ok(entityDto);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteOrIt(int id)
        {
            var entity = _context.OrderItems.FirstOrDefault(x => x.ItemId == id);
            if (entity==null)
            {
                return NoContent();
            }
            entity.IsDeleted=true;
            _context.Update(entity);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("update")]
        public ActionResult<OrderItem> UpdateOrIt(OrderItem orderItem)
        {
            if (orderItem==null)
            {
                return BadRequest();
            }
            var entity = _context.OrderItems.FirstOrDefault(x => x.ItemId == orderItem.ItemId);
            if (entity==null)
            {
                return NoContent();
            }
            entity = orderItem;
            _context.OrderItems.Update(entity);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("create")]
        public ActionResult CreateOrIt(OrderItem orderItem)
        {
            if (orderItem==null)
            {
                return BadRequest();
            }
            var isSame = _context.OrderItems.Any(x => x.ItemId == orderItem.ItemId);
            if (isSame)
            {
                return BadRequest();
            }
            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();
            return Ok();
        }
    }
}
