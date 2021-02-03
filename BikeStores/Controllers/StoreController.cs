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
    public class StoreController : ControllerBase
    {
        BikeStoresContext _context;
        public StoreController()
        {
            _context = new BikeStoresContext();
        }
        [HttpGet()]
        public ActionResult<List<Store>> GetStor()
        {
            var entity = _context.Stores.Where(x=> !x.IsDeleted);
            if (entity == null)
            {
                return NoContent();
            }
            var entityDto = entity.Select(x => new StoreNameDto 
            {
            Phone=x.Phone,
            StoreId=x.StoreId,
            StoreName=x.StoreName          
            });
            return Ok(entityDto);
        }
        [HttpGet("list")]
        public ActionResult<List<Store>> GetStorListDto()
        {
            var entity = _context.Stores.Where(x=> !x.IsDeleted);
            if (entity == null)
            {
                return NoContent();
            }
            var entityDto = entity.Select(x => new StoreListDto
            {
                StaffNameDtos = x.Staff.Select(t => new StaffNameDto
                {
                    FirstName = t.FirstName,
                    LastName=t.LastName,
                    StaffId=t.StaffId
                   
                }).ToList(),
                OrderNameDtos=x.Orders.Select(p=> new OrderNameDto 
                {
                CustomerId=p.CustomerId,
                OrderId=p.OrderId,
                OrderStatus=p.OrderStatus                
                }).ToList(),
                StockNameDtos=x.Stocks.Select(q=> new StockListDto 
                {
                ProductId=q.ProductId,
                StoreId=q.StoreId
                }).ToList()             
            }) ;
            return Ok(entity);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteStor(int id)
        {
            var entity = _context.Stores.FirstOrDefault(x => x.StoreId == id);
            if (entity == null)
            {
                return NoContent();
            }
            foreach (var item in entity.Orders)
            {
                _context.Orders.FirstOrDefault(x => x == item).IsDeleted = true;
            }
            foreach (var item in entity.Stocks)
            {
                _context.Stocks.FirstOrDefault(x => x == item).IsDeleted = true;
            }
            foreach (var item in entity.Staff)
            {
                _context.Staffs.FirstOrDefault(x => x == item).IsDeleted = true;
            }
            _context.Stores.FirstOrDefault(x => x == entity).IsDeleted = true;
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("update")]
        public ActionResult<Store> UpdateStor(Store store)
        {
            if (store == null)
            {
                return BadRequest();
            }
            var entity = _context.Stores.FirstOrDefault(x => x.StoreId == store.StoreId);
            if (entity == null)
            {
                return NoContent();
            }
            entity = store;
            _context.Stores.Update(entity);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("create")]
        public ActionResult CreateStor(Store store)
        {
            if (store == null)
            {
                return BadRequest();
            }
            var isSame = _context.Stores.Any(x => x.StoreId == store.StoreId);
            if (isSame)
            {
                return BadRequest();
            }
            _context.Stores.Add(store);
            _context.SaveChanges();
            return Ok();
        }
    }
}
