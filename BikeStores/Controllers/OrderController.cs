using BikeStores.Data.Entities;
using BikeStores.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStores.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        BikeStoresContext _context;
        public OrderController()
        {
            _context = new BikeStoresContext();
        }
        [HttpGet()]
        public ActionResult<List<Order>> GetOrder()
        {
            var entity = _context.Orders.Where(x=> !x.IsDeleted);
            if (entity==null)
            {
                return NotFound();
            }
            var entityDto = entity.Select(x => new OrderNameDto 
            {
            CustomerId=x.CustomerId,
            OrderId=x.OrderId,
            OrderStatus=x.OrderStatus           
            });
            return Ok(entityDto);
        }
        [HttpGet("list")]
        public ActionResult<List<Order>> GetOrderList()
        {
            var entity = _context.Orders.Where(x=> !x.IsDeleted);
            if (entity == null)
            {
                return NotFound();
            }
            var entityDto = entity.Select(x => new OrderListDto 
            {
            CustomerId=x.CustomerId,
            OrderId=x.OrderId,
            OrderStatus =x.OrderStatus,
            OrderItems =x.OrderItems.Select(t=> new OrderItemListDto 
            {
            ItemId=t.ItemId,
            OrderId=t.OrderId,
            ProductId=t.ProductId            
            }).ToList()           
            });
            return Ok(entity);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteOr(int id)
        {
            var entity = _context.Orders.Include("OrderItems").FirstOrDefault(x => x.OrderId == id);
            if (entity==null)
            {
                return NotFound();
            }
            foreach (var item in entity.OrderItems)
            {
                item.IsDeleted = true;
            }           
            entity.IsDeleted=true;
            _context.Update(entity);
            _context.SaveChanges();
            return Ok("Kayıt silinmiştir.");
        }
        [HttpPut("update")]
        public ActionResult<Order> UpdateOr(Order order)
        {
            if (order == null || order.CustomerId== null || order.OrderDate == null || order.OrderId == 0
                || order.OrderItems == null || order.OrderStatus == 0 || order.RequiredDate == null || order.ShippedDate == null 
                || order.Staff == null || order.StaffId == 0 || order.Store == null || order.StoreId ==0)
            {
                return BadRequest();
            }
            var entity = _context.Orders.FirstOrDefault(x => x.OrderId == order.OrderId);
            if (entity==null)
            {
                return NotFound();
            }
            entity = order;
            _context.Orders.Update(entity);
            _context.SaveChanges();
            return Ok(entity);
        }
        [HttpPost("create")]
        public ActionResult CreateOr(Order order)
        {
            if (order == null || order.CustomerId == null || order.OrderDate == null || order.OrderId == 0
               || order.OrderItems == null || order.OrderStatus == 0 || order.RequiredDate == null || order.ShippedDate == null
               || order.Staff == null || order.StaffId == 0 || order.Store == null || order.StoreId == 0)
            {
                return BadRequest();
            }
            var isSame = _context.Orders.Any(x=> x.OrderId==order.OrderId);
            if (isSame)
            {
                return BadRequest();
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
            return Ok("Kayıt yapılmıştır.");
        } 
    }
}
