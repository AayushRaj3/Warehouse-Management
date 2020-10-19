using Microsoft.AspNetCore.Mvc;
using ShipmentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipmentApi.Repository
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly WarehouseManagementContext _context;

        public ShipmentRepository(WarehouseManagementContext context)
        {
            _context = context;
        }
        public IEnumerable<Shipment> GetShipment()
        {
            return _context.Shipment.ToList();
        }
        public async Task<ActionResult<Shipment>> GetShipment(int id)
        {
            var shipment = await _context.Shipment.FindAsync(id);
            return shipment;
        }
        public async Task<ActionResult<Shipment>> AddShipment([FromBody] Shipment s1)
        {
            var seller = await _context.Seller.FindAsync(s1.SellerId);
            var product = await _context.Product.FindAsync(s1.ProductId);
            if(seller == null || product == null)
            {
                return null;
            }
            var IsAvail = seller.IsAvailable;
            var Quantity = product.ProductQuantity;

            if (Quantity >= s1.Quantity && IsAvail == "Yes")
            {
                s1.Status = "Approved";
                product.ProductQuantity -= s1.Quantity;
                seller.IsAvailable = "No";
            }
            else
            {
                s1.Status = "Cancelled";
            }
            await _context.Shipment.AddAsync(s1);
            await _context.SaveChangesAsync();

            return s1;
        }
        public async Task<ActionResult<Shipment>> DeleteShipment(int id)
        {
            var shipment = await _context.Shipment.FindAsync(id);

            if(shipment == null)
            {
                return null;
            }
            else
            {
                 _context.Shipment.Remove(shipment);
                await _context.SaveChangesAsync();
                return shipment;
            }

        }
    }
}
