using BikeStores.Data.Entities;
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
    public class ProductController : ControllerBase
    {

        BikeStoresContext _context;
        public ProductController()
        {
            _context = new BikeStoresContext();
        }
        [HttpGet("list")]
        public ActionResult<List<Product>> GetPr()
        {
            var entity = _context.Products.ToList();
            if (entity == null)
            {
                return NoContent();
            }
            return Ok(entity);
        }
        [HttpDelete("{id}")]
        public ActionResult DeletePr(int id)
        {
            var entity = _context.Products.FirstOrDefault(x => x.ProductId == id);
            if (entity == null)
            {
                return NoContent();
            }
            foreach (var item in entity.OrderItems)
            {
                _context.OrderItems.Remove(item);
            }
            foreach (var item in entity.Stocks)
            {
                _context.Stocks.Remove(item);
            }
            _context.Products.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("update")]
        public ActionResult<Product> UpdatePr(Product productItem)
        {
            if (productItem == null)
            {
                return BadRequest();
            }
            var entity = _context.Products.FirstOrDefault(x => x.ProductId == productItem.ProductId);
            if (entity == null)
            {
                return NoContent();
            }
            entity = productItem;
            _context.Products.Update(entity);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("create")]
        public ActionResult CreatePr(Product productItem)
        {
            if (productItem == null)
            {
                return BadRequest();
            }
            var isSame = _context.Products.Any(x => x.ProductId == productItem.ProductId);
            if (isSame)
            {
                return BadRequest();
            }
            _context.Products.Add(productItem);
            _context.SaveChanges();
            return Ok();
        }

    }
}
