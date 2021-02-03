using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStores.Models
{
    public class ProductNameDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public short ModelYear { get; set; }
        public decimal ListPrice { get; set; }

    }
    public class ProductListDto: ProductNameDto
    {
        public ProductListDto()
        {
            Stocks = new List<StockListDto>();
            OrderItemListDtos = new List<OrderItemListDto>();
        }
        public List<StockListDto> Stocks { get; set; }
        public List<OrderItemListDto> OrderItemListDtos { get; set; } 
    }
}
