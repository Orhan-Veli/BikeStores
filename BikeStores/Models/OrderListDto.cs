using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStores.Models
{
    public class OrderNameDto
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public byte OrderStatus { get; set; }
    }
    public class OrderListDto:OrderNameDto
    {
        public OrderListDto()
        {
            OrderItems = new List<OrderItemListDto>(); 
        }

        public List<OrderItemListDto> OrderItems { get; set; }
    }
}
