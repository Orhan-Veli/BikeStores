﻿using BikeStores.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStores.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {
        private BikeStoresContext _context;
        public BrandController()
        {
            _context = new BikeStoresContext();
        }
        [HttpGet("list")]
        public ActionResult<List<Brand>> GetBrands()
        {
            var brandList = _context.Brands.ToList();
            if (brandList.Count==0)
            {
                return BadRequest();
            }
            return Ok(brandList);
        }
        
        [HttpPut("update")]
        public ActionResult<Brand> PutBrand([FromBody]Brand model)
        {
            
            if (model==null || model.BrandName==null || model.BrandId==0)
            {
                return BadRequest();
            }
            var entity = _context.Brands.FirstOrDefault(x=> x.BrandId==model.BrandId);
            if (entity==null)
            {
                return NotFound();
            }
            entity.BrandName = model.BrandName;
            _context.Brands.Update(entity);
            _context.SaveChanges();
            return Ok(entity);            
        }
        [HttpPost("create")]
        public ActionResult PostBrand([FromBody]Brand model)
        {
            if (model==null || model.BrandId==0 || model.BrandName==null)
            {
                return BadRequest("Hatalı model");
            }
            var isSame = _context.Brands.Any(x => x.BrandName == model.BrandName);
            if(isSame)
            {
                return BadRequest("Kayıt zaten var.");
            }
            _context.Brands.Add(model);
            _context.SaveChanges();
            return Ok("Kayıt eklendi.");
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteBrand(int id)
        {
            var entity = _context.Brands.FirstOrDefault(x => x.BrandId == id);
            if (entity==null)
            {
                return NotFound("Kayıt bulunamadı.");
            }
            foreach (var item in entity.Products)
            {
                _context.Products.Remove(item);
            }
            _context.Brands.Remove(entity);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
