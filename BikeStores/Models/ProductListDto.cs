using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStores.Models
{
    public class ProductListDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public short ModelYear { get; set; }
        public decimal ListPrice { get; set; }

    }
}
