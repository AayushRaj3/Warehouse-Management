using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Repository;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ProductController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProductController));

        public IProductRepository repo;

        public ProductController(IProductRepository repo)
        {
            this.repo = repo;
        }        

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult GetProduct()
        {
            _log4net.Info("ProductController HttpGet ");
            try
            {
                var details = repo.GetProduct();
                return Ok(details);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //return repo.GetProduct();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            _log4net.Info("ProductController HttpGet By ID ");
            if (id == 0)
            {
                return BadRequest("Provide Valid ID");
            }
            try
            {
                var product = await repo.GetProduct(id);
                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    return product;
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct([FromBody]Product p1)
        {
            _log4net.Info("ProductController HttpPost AddProduct ");
            try
            {
                var result = await repo.AddProduct(p1);
                if (result != null)
                {
                    return p1;
                }
                else
                {
                    return BadRequest("Product cannot be added");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // PUT api/<ProductController>
        [HttpPut]
        [Route("ReplaceProduct")]
        public IActionResult ReplaceProduct(int id,[FromBody] Product p1)
        {
            _log4net.Info("ProductController HttpPut ");
            if (id == 0 || p1 == null)
            {
                return BadRequest("Invalid ID");
            }
            try
            {
                var product = repo.ReplaceProduct(id, p1);
                if (product == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(p1);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // DELETE api/<ProductController>/5
        [HttpDelete]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            _log4net.Info("ProductController HttpDelete ");
            if (id == 0)
            {
                return BadRequest("Invalid ID");
            }
            try
            {
                var product = await repo.DeleteProduct(id);

                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    return product;
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
