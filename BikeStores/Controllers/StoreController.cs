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
    public class StoreController : ControllerBase
    {
        BikeStoresContext _context;
        public StoreController()
        {
            _context = new BikeStoresContext();
        }
        [HttpGet("list")]
        public ActionResult<List<Store>> GetStor()
        {
            var entity = _context.Stores.ToList();
            if (entity == null)
            {
                return NoContent();
            }
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
                _context.Orders.Remove(item);
            }
            foreach (var item in entity.Stocks)
            {
                _context.Stocks.Remove(item);
            }
            foreach (var item in entity.Staff)
            {
                _context.Staffs.Remove(item);
            }
            _context.Stores.Remove(entity);
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
