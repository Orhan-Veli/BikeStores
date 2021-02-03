using BikeStores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStores.Models
{
    public class CustomerNameDto
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class CustomerListDto : CustomerNameDto
    {
        public CustomerListDto()
        {
            Orders = new List<OrderListDto>();
        }
        public List<OrderListDto> Orders { get; set; }
    }
}
