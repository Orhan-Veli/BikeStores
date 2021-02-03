using BikeStores.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStores.Controllers
{
    [ApiController]
    [Route("[controller]")]   
    public class CustomerController : ControllerBase
    {
        BikeStoresContext _context;
        public CustomerController()
        {
            _context = new BikeStoresContext();
        }
        [HttpGet("customer")]
        public ActionResult<List<Customer>> GetCustomer()
        {
          
            var entity= _context.Customers.ToList();
            if (entity==null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
        [HttpDelete("{id}")]
        public ActionResult CustomerDelete(int id)
        {
            var entity = _context.Customers.FirstOrDefault(x=> x.CustomerId==id);
            if (entity==null)
            {
                return NotFound();
            }
            foreach (var item in entity.Orders)
            {
                _context.Orders.Remove(item);
            }
            _context.Customers.Remove(entity);
            _context.SaveChanges();
            return Ok("Kayıt silinmiştir.");
        }
        [HttpPost("create")]
        public ActionResult CreateCus(Customer customer)
        {
            if (customer==null || customer.City == null || customer.CustomerId == 0 || customer.Email == null || customer.FirstName == null || customer.LastName == null ||
                customer.Orders == null || customer.Phone == null || customer.State == null || customer.Street == null || customer.ZipCode == null)
            {
                return BadRequest();
            }
            var isSame = _context.Customers.Any(x => x.CustomerId == customer.CustomerId);
            if (isSame)
            {
                return BadRequest();
            }
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok("Kayıt eklenmiştir.");
        }
        [HttpPut("update")]
        public ActionResult<Customer> UpdateCus(Customer customer)
        {
            if (customer == null || customer.City == null || customer.CustomerId == 0 || customer.Email == null || customer.FirstName == null || customer.LastName == null ||
                customer.Orders == null || customer.Phone == null || customer.State == null || customer.Street == null || customer.ZipCode == null)
            {
                return BadRequest();
            }
            var entity = _context.Customers.FirstOrDefault(x => x.CustomerId == customer.CustomerId);
            if (entity==null)
            {
                return NotFound();
            }
            entity = customer;
            _context.Customers.Update(entity);
            _context.SaveChanges();
            return Ok(entity);
        }
    }
}
