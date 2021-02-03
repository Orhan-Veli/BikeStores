using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStores.Models
{
    public class StaffListDto: StaffNameDto
    {
        public StaffListDto()
        {
            
            OrderListDtos = new List<OrderNameDto>();
        }      
        public List<OrderNameDto> OrderListDtos { get; set; }
    }
    public class StaffNameDto
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
