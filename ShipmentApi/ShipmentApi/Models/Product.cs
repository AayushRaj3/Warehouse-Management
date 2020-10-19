using System;
using System.Collections.Generic;

namespace ShipmentApi.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductCost { get; set; }
    }
}
