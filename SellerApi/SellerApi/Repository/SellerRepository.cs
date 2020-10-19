using Microsoft.AspNetCore.Mvc;
using SellerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerApi.Repository
{
    public class SellerRepository:ISellerRepository
    {
        private readonly WarehouseManagementContext _context;

        public SellerRepository(WarehouseManagementContext context)
        {
            _context = context;
        }
        public List<Seller> GetSeller()
        {
            return _context.Seller.ToList();
        }

        public async Task<ActionResult<Seller>> GetSeller(int id)
        {
            var seller = await _context.Seller.FindAsync(id);

            if (seller == null)
            {
                return null;
            }
            else
            {
                return seller;
            }
        }

        public IEnumerable<Seller> GetSellerByLocation(string Location)
        {
            return _context.Seller.Where(x => x.Address == Location);
        }

        public IEnumerable<Seller> GetSellerByAvailability()
        {
            return _context.Seller.Where(x => x.IsAvailable == "Yes");
        }
        public async Task<ActionResult<Seller>> AddSeller([FromBody] Seller s1)
        {
            await _context.Seller.AddAsync(s1);
            await _context.SaveChangesAsync();

            return s1;
        }
        public async Task<ActionResult<Seller>> SellerUpdate(int id, [FromBody] Seller s1)
        {
            var seller = await _context.Seller.FindAsync(id);
            if (seller == null)
            {
                return null;
            }
            else
            {
                seller.SellerName = s1.SellerName;
                seller.IsAvailable = s1.IsAvailable;
                seller.Phone = s1.Phone;
                seller.Address = s1.Address;
                await _context.SaveChangesAsync();
                return seller;
            }
        }
        public async Task<ActionResult<Seller>> DeleteSeller(int id)
        {
            var seller = await _context.Seller.FindAsync(id);

            if (seller == null)
            {
                return null;
            }
            else
            {
                _context.Seller.Remove(seller);
                await _context.SaveChangesAsync();
                return seller;
            }
        }
    }
}
