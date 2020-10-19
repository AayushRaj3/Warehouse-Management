using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseClient.Models;

namespace WarehouseClient.Controllers
{
    public class SellerController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SellerController));

        public async Task<IActionResult> Index()
        {
            _log4net.Info("SellerController List of Sellers");
            try
            {
                var seller = new List<Seller>();
                using (var httpclient = new HttpClient())
                {
                    httpclient.BaseAddress = new Uri("https://localhost:44383/");
                    HttpResponseMessage res = await httpclient.GetAsync("api/Seller");
                    if (res.IsSuccessStatusCode)
                    {
                        var results = res.Content.ReadAsStringAsync().Result;
                        seller = JsonConvert.DeserializeObject<List<Seller>>(results);
                    }
                }
                return View(seller);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            _log4net.Info("SellerController Details of Seller using Specific ID");
            try
            {
                var seller = new Seller();
                using (var httpclient = new HttpClient())
                {
                    httpclient.BaseAddress = new Uri("https://localhost:44383/");
                    HttpResponseMessage res = await httpclient.GetAsync($"api/Seller/{id}");
                    if (res.IsSuccessStatusCode)
                    {
                        _log4net.Info("SellerController Seller By ID Found");
                        var results = res.Content.ReadAsStringAsync().Result;
                        seller = JsonConvert.DeserializeObject<Seller>(results);
                    }
                    return View(seller);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public ActionResult Add()
        {
            _log4net.Info("SellerController Trying to Add Seller");
            return View();
        }
        [HttpPost]
        public IActionResult Add(Seller seller)
        {
            try
            {
                using (var httpclient = new HttpClient())
                {
                    httpclient.BaseAddress = new Uri("https://localhost:44383/");
                    var postTask = httpclient.PostAsJsonAsync<Seller>("api/seller", seller);

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        _log4net.Info("SellerController Seller Added");
                        return RedirectToAction("Index");
                    }
                    return View(seller);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                using (var httpclient = new HttpClient())
                {
                    _log4net.Info("SellerController Trying to Delete a seller");
                    httpclient.BaseAddress = new Uri("https://localhost:44383/");
                    var delete = httpclient.DeleteAsync("/api/Seller?id=" + id);
                    var res = delete.Result;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
