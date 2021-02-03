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
    public class StaffController : ControllerBase
    {
        BikeStoresContext _context;
        public StaffController()
        {
            _context = new BikeStoresContext();
        }
        [HttpGet("list")]
        public ActionResult<List<Staff>> GetOrIt()
        {
            var entity = _context.Staffs.ToList();
            if (entity == null)
            {
                return NoContent();
            }
            return Ok(entity);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteSt(int id)
        {
            var entity = _context.Staffs.FirstOrDefault(x => x.StaffId == id);
            if (entity == null)
            {
                return NoContent();
            }
            foreach (var item in entity.InverseManager)
            {
                _context.Staffs.Remove(item);
            }
            foreach (var item in entity.Orders)
            {
                _context.Orders.Remove(item);
            }          
            _context.Staffs.Remove(entity);
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
