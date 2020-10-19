using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SellerApi.Models;
using SellerApi.Repository;

namespace SellerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class SellerController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SellerController));

        public ISellerRepository repo;

        public SellerController(ISellerRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/<SellerController>
        [HttpGet]
        public IActionResult GetSeller()
        {
            _log4net.Info("SellerController HttpGet ");
            
            var details = repo.GetSeller();
            return Ok(details);
        }

        // GET api/<SellerController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Seller>> GetSeller(int id)
        {
            _log4net.Info("SellerController HttpGet By Id ");
            if (id == 0)
            {
                return BadRequest("Invalid Seller ID");
            }
            try
            {
                var seller = await repo.GetSeller(id);

                if (seller == null)
                {
                    return NotFound();
                }
                else
                {
                    return seller;
                }
            }
            
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<SellerController>/GetSellerByLocation/Mumbai
        [Route("[action]/{Location}")]
        [HttpGet]
        public IEnumerable<Seller> GetSellerByLocation(string Location)
        {
            _log4net.Info("SellerController HttpGet By Location ");
            try
            {
                return repo.GetSellerByLocation(Location);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<SellerController>/GetSellerByAvailability/Yes
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Seller> GetSellerByAvailability()
        {
            _log4net.Info("SellerController HttpGet By Availability ");
            try
            {
                return repo.GetSellerByAvailability();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // POST api/<SellerController>
        [HttpPost]
        public async Task<ActionResult<Seller>> AddSeller([FromBody] Seller s1)
        {
            _log4net.Info("SellerController HttpPost ");
            try
            {
                return await repo.AddSeller(s1);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<SellerController>
        [HttpPut]
        public async Task<ActionResult<Seller>> SellerUpdate(int id, [FromBody] Seller s1)
        {
            _log4net.Info("SellerController HttpPut ");
            if (id == 0)
            {
                return BadRequest("Invalid Seller ID");
            }
            if(id != s1.SellerId)
            {
                return BadRequest("Seller ID did not match");
            }
            try
            {
                var seller = await repo.SellerUpdate(id,s1);
                if (seller == null)
                {
                    return NotFound();
                }
                else
                {
                    return seller;
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // DELETE api/<SellerController>/5
        [HttpDelete]
        public async Task<ActionResult<Seller>> DeleteSeller(int id)
        {
            _log4net.Info("SellerController HttpDelete ");
            if (id == 0)
            {
                return BadRequest("Invalid Seller ID");
            }
            try
            {
                var seller = await repo.DeleteSeller(id);

                if (seller == null)
                {
                    return NotFound();
                }
                else
                {
                    return seller;
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
