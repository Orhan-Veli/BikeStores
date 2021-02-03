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
    public class StockController : ControllerBase
    {
        BikeStoresContext _context;
        public StockController()
        {
            _context = new BikeStoresContext();
        }
        [HttpGet()]
        public ActionResult<List<Stock>> GetSto()
        {
            var entity = _context.Stocks.Where(x => !x.IsDeleted);
            if (entity == null)
            {
                return NoContent();
            }
            var entityDto = entity.Select(x => new StockListDto 
            {
            ProductId=x.ProductId,
            StoreId=x.StoreId 
            });
            return Ok(entity);
        }       
        [HttpDelete("{id}")]
        public ActionResult DeleteSto(int id)
        {
            var entity = _context.Stocks.FirstOrDefault(x => x.Quantity == id);
            if (entity == null)
            {
                return NoContent();
            }
            _context.Stocks.FirstOrDefault(x => x == entity).IsDeleted = true;
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("update")]
        public ActionResult<OrderItem> UpdateOrIt(OrderItem orderItem)
        {
            if (orderItem == null)
            {
                return BadRequest();
            }
            var entity = _context.OrderItems.FirstOrDefault(x => x.ItemId == orderItem.ItemId);
            if (entity == null)
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
            if (orderItem == null)
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
