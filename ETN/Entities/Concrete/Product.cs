using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Product
    {
        public int Id { get; set; }

        public int? ProductCode { get; set; }

        public string ProductName { get; set; }

        //public ICollection<ProductOrder> ProductOrders { get; set; }
        public IEnumerable<ProductOrder> ProductOrders { get; set; }
    }
}
