using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStores.Models
{
    public class BrandListDto:BrandNameDto
    {
        public BrandListDto()
        {
            Products= new List<ProductNameDto>();
        }
        public List<ProductNameDto> Products { get; set; }
    }
    public class BrandNameDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
}
