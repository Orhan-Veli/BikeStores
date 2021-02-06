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
    public class StaffController : ControllerBase
    {
        BikeStoresContext _context;
        public StaffController()
        {
            _context = new BikeStoresContext();
        }
        [HttpGet()]
        public ActionResult<List<Staff>> GetOrIt()
        {
            var entity = _context.Staffs.Where(x=> !x.IsDeleted);
            if (entity == null)
            {
                return NoContent();
            }
            var entityDto = entity.Select(x=> new StaffNameDto 
            {
            FirstName=x.FirstName,
            LastName=x.LastName,
            StaffId=x.StaffId            
            });
            return Ok(entityDto);
        }
        [HttpGet("list")]
        public ActionResult<List<Staff>> GetOrItListDto()
        {
            var entity = _context.Staffs.Where(x=> !x.IsDeleted);
            if (entity == null)
            {
                return NoContent();
            }
            var entityDto = entity.Select(x => new StaffListDto
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                StaffId = x.StaffId,
                OrderListDtos = x.Orders.Select(t => new OrderNameDto 
                {
                CustomerId=t.CustomerId,
                OrderId=t.OrderId,
                OrderStatus=t.OrderStatus                
                }).ToList()
            });
            return Ok(entityDto);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteSt(int id)
        {
            var entity = _context.Staffs.Include("InverseManager").Include("Orders").FirstOrDefault(x => x.StaffId == id);
            if (entity == null)
            {
                return NoContent();
            }
            foreach (var item in entity.InverseManager)
            {
                item.IsDeleted = true;
            }
            foreach (var item in entity.Orders)
            {
                 item.IsDeleted = true;
            }
            entity.IsDeleted = true;
            _context.Update(entity);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("update")]
        public ActionResult<Staff> UpdateSt(Staff staff)
        {
            if (staff == null)
            {
                return BadRequest();
            }
            var entity = _context.Staffs.FirstOrDefault(x => x.StaffId == staff.StaffId);
            if (entity == null)
            {
                return NoContent();
            }
            entity = staff;
            _context.Staffs.Update(entity);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("create")]
        public ActionResult CreateSt(Staff staff)
        {
            if (staff == null)
            {
                return BadRequest();
            }
            var isSame = _context.Staffs.Any(x => x.StaffId == staff.StaffId);
            if (isSame)
            {
                return BadRequest();
            }
            _context.Staffs.Add(staff);
            _context.SaveChanges();
            return Ok();
        }

    }
}
