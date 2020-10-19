using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseClient.Helper;
using WarehouseClient.Models;

namespace WarehouseClient.Controllers
{
    public class ShipmentController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ShipmentController));

        public async Task<IActionResult> Index()
        {
            try
            {
                _log4net.Info("ShipmentController List of Shipment Successfull");
                var shipment = new List<Shipment>();
                using (var httpclient = new HttpClient())
                {
                    httpclient.BaseAddress = new Uri("https://localhost:44301/");
                    HttpResponseMessage res = await httpclient.GetAsync("api/Shipment");
                    if (res.IsSuccessStatusCode)
                    {
                        var results = res.Content.ReadAsStringAsync().Result;
                        shipment = JsonConvert.DeserializeObject<List<Shipment>>(results);
                    }
                }
                return View(shipment);
            }

            catch (Exception ex)
            {
                _log4net.Info("ShipmentController List of Shipment Failed");
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                _log4net.Info("ShipmentController Shipment By ID is Successfull");
                var shipment = new Shipment();
                using(var httpclient = new HttpClient())
                {
                    httpclient.BaseAddress = new Uri("https://localhost:44301/");
                    HttpResponseMessage res = await httpclient.GetAsync($"api/Shipment/{id}");
                    if (res.IsSuccessStatusCode)
                    {
                        var results = res.Content.ReadAsStringAsync().Result;
                        shipment = JsonConvert.DeserializeObject<Shipment>(results);
                    }
                    return View(shipment);
                }
                
            }
            catch (Exception ex)
            {
                _log4net.Info("ShipmentController Shipment by ID Failed");
                return BadRequest(ex.Message);
            }
        }
        public ActionResult Add()
        {
            _log4net.Info("ShipmentController Trying to Add Shipment");
            return View();
        }
        [HttpPost]
        public IActionResult Add(Shipment shipment)
        {
            try
            {
                _log4net.Info("ShipmentController Shipment Successfully Added");
                using (var httpclient = new HttpClient())
                {
                    httpclient.BaseAddress = new Uri("https://localhost:44301/");
                    var postTask = httpclient.PostAsJsonAsync<Shipment>("api/shipment", shipment);

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(shipment);
                }
            }
            catch(Exception ex)
            {
                _log4net.Info("ShipmentController Shipment cannot be Added");
                return BadRequest(ex.Message);
            }
        }
        public IActionResult Delete(int id)
        {
            try
            {
                _log4net.Info("ShipmentController Deleting Shipment");
                using (var httpclient = new HttpClient())
                {
                    httpclient.BaseAddress = new Uri("https://localhost:44301/");
                    var delete = httpclient.DeleteAsync("/api/Shipment?id=" + id);
                    var res = delete.Result;
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                _log4net.Info("ShipmentController Shipment Deletion Failed");
                return BadRequest(ex.Message);
            }
        }
    }
}
