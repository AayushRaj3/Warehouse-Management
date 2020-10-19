using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Repository
{
    public interface IProductRepository
    {
        public List<Product> GetProduct();
        public Task<ActionResult<Product>> GetProduct(int id);
        public Task<ActionResult<Product>> AddProduct(Product p1);
        public int ReplaceProduct(int id, Product p1);
        public Task<ActionResult<Product>> DeleteProduct(int id);
    }
}
