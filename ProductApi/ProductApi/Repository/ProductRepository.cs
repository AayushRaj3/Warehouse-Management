using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Repository;

namespace ProductApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly WarehouseManagementContext _context;

        public ProductRepository(WarehouseManagementContext context)
        {
            _context = context;
        }

        public List<Product> GetProduct()
        {
            return _context.Product.ToList();
        }

        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            return product;
        }

        public async Task<ActionResult<Product>> AddProduct(Product p1)
        {
            await _context.Product.AddAsync(p1);
            await _context.SaveChangesAsync();

            return p1;
        }
        public int ReplaceProduct(int id, Product p1)
        {
            var product = _context.Product.FirstOrDefault(p => p.ProductId == id);
            product.ProductId = p1.ProductId;
            product.ProductName = p1.ProductName;
            product.ProductQuantity = p1.ProductQuantity;
            product.ProductCost = p1.ProductCost;
            return _context.SaveChanges();
            
        }

        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return null;
            }
            else
            {
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
                return product;
            }
        }

    }
}
