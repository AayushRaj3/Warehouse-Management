using System;
using System.Collections.Generic;

namespace AuthenticationApi.Models
{
    public partial class Shipment
    {
        public int ShipmentId { get; set; }
        public int SellerId { get; set; }
        public int ProductId { get; set; }
        public string ToLocation { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}
