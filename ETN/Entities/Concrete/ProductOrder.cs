using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class ProductOrder
    {  

        public int PId { get; set; }
        public Product Product { get; set; }

        public int OId { get; set; }
        public Order Order { get; set; }
    }
}
