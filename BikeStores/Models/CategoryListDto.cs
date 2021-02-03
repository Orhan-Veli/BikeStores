using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStores.Models
{
    public class CategoryNameDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    public class CategoryListDto : CategoryNameDto
    {
        public CategoryListDto()
        {
            Products = new List<ProductListDto>();
        }
        public List<ProductListDto> Products { get; set; }
    }
}
