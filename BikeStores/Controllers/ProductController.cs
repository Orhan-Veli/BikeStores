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
    public class ProductController : ControllerBase
    {

        BikeStoresContext _context;
        public ProductController()
        {
            _context = new BikeStoresContext();
        }
        [HttpGet()]
        public ActionResult<List<Product>> GetPr()
        {
            var entity = _context.Products.Where(x=> !x.IsDeleted);
            if (entity == null)
            {
                return NoContent();
            }
            var entityDto = entity.Select(x => new ProductNameDto 
            {
            ListPrice=x.ListPrice,
            ModelYear=x.ModelYear,
            ProductId=x.ProductId,
            ProductName=x.ProductName           
            });
            return Ok(entityDto);
        }
        [HttpGet("list")]
        public ActionResult<List<Product>> GetPrListDto()
        {
            var entity = _context.Products.Where(x=> !x.IsDeleted);
            if (entity == null)
            {
                return NoContent();
            }
            var entityDto = entity.Select(x => new ProductListDto 
            {
            ListPrice=x.ListPrice,
            ModelYear=x.ModelYear,
            ProductId=x.ProductId,
            ProductName=x.ProductName,
            OrderItemListDtos=x.OrderItems.Select(t=> new OrderItemListDto 
            {
            ItemId=t.ItemId,
            OrderId=t.OrderId,
            ProductId=t.ProductId            
            }).ToList(),
            Stocks=x.Stocks.Select(p=> new StockListDto 
            {
            ProductId=p.ProductId,
            StoreId=p.StoreId            
            }).ToList()           
            
            });
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
                _context.OrderItems.FirstOrDefault(x => x == item).IsDeleted = true;
            }
            foreach (var item in entity.Stocks)
            {
                _context.Stocks.FirstOrDefault(x => x == item).IsDeleted = true;
            }
            _context.Products.FirstOrDefault(x => x == entity).IsDeleted = true;
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
