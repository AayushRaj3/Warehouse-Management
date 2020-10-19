using System;
using System.Collections.Generic;

namespace ShipmentApi.Models
{
    public partial class Seller
    {
        public int SellerId { get; set; }
        public string SellerName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string IsAvailable { get; set; }
    }
}
