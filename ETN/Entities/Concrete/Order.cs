using Castle.Core.Resource;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Order
    {
        public int Id { get; set; }
        public int? OrderNo { get; set; }
        public string OutgoingAddress { get; set; }
        public string DestinationAddress { get; set; }
        public int? Quantity { get; set; }
        public string UnitOfQuantity { get; set; }
        public int? Weight { get; set; }
        public string UnitOfWeight { get; set; }
        public int? ProductCode { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }

        public IEnumerable<ProductOrder> ProductOrders { get; set; }
    }
}
