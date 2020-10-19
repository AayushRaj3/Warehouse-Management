using Microsoft.AspNetCore.Mvc;
using ShipmentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipmentApi.Repository
{
    public interface IShipmentRepository
    {
        public IEnumerable<Shipment> GetShipment();
        public Task<ActionResult<Shipment>> GetShipment(int id);
        public Task<ActionResult<Shipment>> AddShipment(Shipment s1);
        public Task<ActionResult<Shipment>> DeleteShipment(int id);
    }
}
