using Microsoft.AspNetCore.Mvc;
using SellerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerApi.Repository
{
    public interface ISellerRepository
    {
        public List<Seller> GetSeller();
        public Task<ActionResult<Seller>> GetSeller(int id);
        public IEnumerable<Seller> GetSellerByLocation(string Location);
        public IEnumerable<Seller> GetSellerByAvailability();
        public Task<ActionResult<Seller>> AddSeller(Seller s1);
        public Task<ActionResult<Seller>> SellerUpdate(int id, Seller s1);
        public Task<ActionResult<Seller>> DeleteSeller(int id);
    }
}
