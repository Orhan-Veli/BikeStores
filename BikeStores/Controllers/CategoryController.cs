using BikeStores.Data.Entities;
using BikeStores.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStores.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        BikeStoresContext _context;
        public CategoryController()
        {
            _context = new BikeStoresContext();
        }
        [HttpGet]
        public ActionResult GetCategoryNames()
        {
            var entity = _context.Categories.Where(x => !x.IsDeleted);
            if (entity == null)
            {
                return BadRequest();
            }
            var dto = entity.Select(x => new CategoryNameDto
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName              
            });
            return Ok(dto);
        }
        [HttpGet("list")]
        public ActionResult GetCategoriesWithProducts()
        {
            var entity = _context.Categories.Where(x => !x.IsDeleted);
            if (entity == null)
            {
                return BadRequest();
            }
            var dto = entity.Select(x => new CategoryListDto
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                Products = x.Products.Select(p => new ProductListDto
                {
                    ProductId = p.ProductId,
                    ListPrice = p.ListPrice,
                    ModelYear = p.ModelYear,
                    ProductName = p.ProductName
                }).ToList()
            
            });
            return Ok(dto);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteControl(int id)
        {
            var entity = _context.Categories.Include("Products").Include("OrderItems").FirstOrDefault(x => x.CategoryId == id);
            if (entity == null)
            {
                return NotFound();
            }
            foreach (var item in entity.Products)
            {
                foreach (var order in item.OrderItems)
                {
                    order.IsDeleted = true;
                }
               item.IsDeleted = true;
            }
            entity.IsDeleted = true;
            _context.Update(entity);
            _context.SaveChanges();
            return Ok("Id silinmiştir.");
        }
        [HttpPut("update")]
        public ActionResult<Category> UpdateVal([FromBody] Category model)
        {

            if (model == null || model.CategoryName == null || model.CategoryId == 0)
            {
                return BadRequest();
            }
            var entity = _context.Categories.FirstOrDefault(x => x.CategoryId == model.CategoryId);
            if (entity == null)
            {
                return NotFound();
            }
            entity.CategoryId = model.CategoryId;
            entity.CategoryName = model.CategoryName;
            _context.Categories.Update(entity);
            _context.SaveChanges();
            return Ok(entity);
        }
        [HttpPost("create")]
        public ActionResult<Category> PostCreate([FromBody] Category model)
        {
            if (model == null || model.CategoryId == 0 || model.CategoryName == null)
            {
                return BadRequest();
            }
            var isSame = _context.Categories.Any(x => x.CategoryId == model.CategoryId && x.CategoryName == model.CategoryName);
            if (isSame)
            {
                return BadRequest();
            }
            _context.Categories.Add(model);
            _context.SaveChanges();
            return Ok("Kayıt Eklendi");
        }
    }
}
