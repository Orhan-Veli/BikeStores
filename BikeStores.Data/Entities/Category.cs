using System;
using System.Collections.Generic;

#nullable disable

namespace BikeStores.Data.Entities
{
    public partial class Category:BaseEntity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
