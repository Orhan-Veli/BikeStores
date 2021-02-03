using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStores.Models
{
    public class StoreListDto:StaffNameDto
    {
        public StoreListDto()
        {
            OrderNameDtos = new List<OrderNameDto>();
            StockNameDtos = new List<StockListDto>();
            StaffNameDtos = new List<StaffNameDto>();
        }
        public List<OrderNameDto> OrderNameDtos { get; set; }
        public List<StockListDto> StockNameDtos { get; set; }

        public List<StaffNameDto> StaffNameDtos { get; set; }
    }
    public class StoreNameDto
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Phone { get; set; }
    }
}
