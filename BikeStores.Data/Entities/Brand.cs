using System;
using System.Collections.Generic;

#nullable disable

namespace BikeStores.Data.Entities
{
    public partial class Brand:BaseEntity
    {
        public Brand()
        {
            Products = new HashSet<Product>();
        }

        public int BrandId { get; set; }
        public string BrandName { get; set; }      
        public virtual ICollection<Product> Products { get; set; }
    }
}
