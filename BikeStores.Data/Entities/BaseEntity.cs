using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStores.Data.Entities
{
    public class BaseEntity
    {
        public bool IsDeleted { get; set; } = false;
    }
}
