using BikeStores.Data.Entities;
using BikeStores.Models;
using Microsoft.AspNetCore.Mvc;
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
        BikeStoresContext _bikeStores;
        public CategoryController()
        {
            _bikeStores = new BikeStoresContext();
        }
        [HttpGet]
        public ActionResult GetCategoryNames()
        {
            var entity = _bikeStores.Categories.Where(x => !x.IsDeleted);
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
            var entity = _bikeStores.Categories.Where(x => !x.IsDeleted);
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
            var deleteCategory = _bikeStores.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (deleteCategory == null)
            {
                return NotFound();
            }
            foreach (var item in deleteCategory.Products)
            {
                foreach (var order in item.OrderItems)
                {
                    _bikeStores.OrderItems.FirstOrDefault(x => x == order).IsDeleted = true;
                }
                _bikeStores.Products.FirstOrDefault(x => x == item).IsDeleted = true;
            }
            _bikeStores.Categories.FirstOrDefault(x => x == deleteCategory).IsDeleted = true;
            _bikeStores.SaveChanges();
            return Ok("Id silinmiştir.");
        }
        [HttpPut("update")]
        public ActionResult<Category> UpdateVal([FromBody] Category model)
        {

            if (model == null || model.CategoryName == null || model.CategoryId == 0)
            {
                return BadRequest();
            }
            var entity = _bikeStores.Categories.FirstOrDefault(x => x.CategoryId == model.CategoryId);
            if (entity == null)
            {
                return NotFound();
            }
            entity.CategoryId = model.CategoryId;
            entity.CategoryName = model.CategoryName;
            _bikeStores.Categories.Update(entity);
            _bikeStores.SaveChanges();
            return Ok(entity);
        }
        [HttpPost("create")]
        public ActionResult<Category> PostCreate([FromBody] Category model)
        {
            if (model == null || model.CategoryId == 0 || model.CategoryName == null)
            {
                return BadRequest();
            }
            var isSame = _bikeStores.Categories.Any(x => x.CategoryId == model.CategoryId && x.CategoryName == model.CategoryName);
            if (isSame)
            {
                return BadRequest();
            }
            _bikeStores.Categories.Add(model);
            _bikeStores.SaveChanges();
            return Ok("Kayıt Eklendi");
        }
    }
}
