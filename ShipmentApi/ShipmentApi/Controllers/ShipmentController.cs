using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipmentApi.Models;
using ShipmentApi.Repository;

namespace ShipmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ShipmentController : ControllerBase
    {

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ShipmentController));
        public IShipmentRepository repo;
        public ShipmentController(IShipmentRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/<ShipmentController>
        [HttpGet]
        public IEnumerable<Shipment> GetShipment()
        {
            _log4net.Info("ShipmentController HttpGet ");
            return repo.GetShipment();
        }

        // GET api/<ShipmentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shipment>> GetShipment(int id)
        {
            _log4net.Info("ShipmentController HttpGet by ID");
            var shipment = await repo.GetShipment(id);
            try
            {
                if (shipment == null)
                {
                    return NotFound();
                }
                else
                {
                    return shipment;

                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ShipmentController>
        [HttpPost]
        public async Task<ActionResult<Shipment>> AddShipment([FromBody] Shipment s1)
        {
            _log4net.Info("ShipmentController HttpPost ");
            try
            {
                var shipment = await repo.AddShipment(s1);
                if(s1 == null)
                {
                    return BadRequest("Shipment cannot be added !!");
                }
                else
                {
                    return s1;
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ShipmentController>/5
        [HttpDelete]
        public async Task<ActionResult<Shipment>> DeleteShipment(int id)
        {
            _log4net.Info("ShipmentController HttpDelete ");
            if (id == 0)
            {
                return BadRequest("Invalid Shipment");
            }
            try
            {
                var shipment = await repo.DeleteShipment(id);

                if (shipment == null)
                {
                    return NotFound();
                }
                else
                {
                    return shipment;
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
