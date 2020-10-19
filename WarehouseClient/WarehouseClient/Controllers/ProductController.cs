using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseClient.Helper;
using WarehouseClient.Models;

namespace WarehouseClient.Controllers
{
    public class ProductController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProductController));

        readonly ProductAPI _api = new ProductAPI();
        public async Task<IActionResult> Index()
        {
            try
            {
                _log4net.Info("ProductController List of Product Successfull");
                var product = new List<Product>();
                using (var httpclient = new HttpClient())
                {
                    httpclient.BaseAddress = new Uri("https://localhost:44329/");
                    HttpResponseMessage res = await httpclient.GetAsync("api/Product");
                    if (res.IsSuccessStatusCode)
                    {
                        var results = res.Content.ReadAsStringAsync().Result;
                        product = JsonConvert.DeserializeObject<List<Product>>(results);
                    }
                }
                return View(product);
                /*
                List<Product> products = new List<Product>();
                HttpClient client = _api.Initial();
                HttpResponseMessage res = await client.GetAsync("api/Product");
                if (res.IsSuccessStatusCode)
                {
                    var results = res.Content.ReadAsStringAsync().Result;
                    products = JsonConvert.DeserializeObject<List<Product>>(results);
                }
                return View(products);
                */
            }
            catch(Exception ex)
            {
                _log4net.Info("ProductController List of Product Failed");
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                _log4net.Info("ProductController Getting Product By ID Successfull");
                var product = new Product();
                HttpClient client = _api.Initial();
                HttpResponseMessage res = await client.GetAsync($"api/Product/{id}");
                if (res.IsSuccessStatusCode)
                {
                    var results = res.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<Product>(results);
                }
                return View(product);
            }
            catch(Exception ex)
            {
                _log4net.Info("ProductController Getting Product By ID Failed");
                return BadRequest(ex.Message);
            }
        }

        public ActionResult Add()
        {
            _log4net.Info("ProductController Trying to Add Product");
            return View();
        }
        [HttpPost]
        public IActionResult Add(Product product)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<Product>("api/product",product);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                _log4net.Info("ProductController Product Added Successfully");
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Product product = new Product();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Product/{id}");
            if (res.IsSuccessStatusCode)
            {
                _log4net.Info("ProductController Trying to Edit the Product Details");
                var results = res.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<Product>(results);
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            HttpClient client = _api.Initial();
            var postdata = client.PutAsJsonAsync("/api/Product/ReplaceProduct?id="+product.ProductId, product);
            var res = postdata.Result;
            if (res.IsSuccessStatusCode)
            {
                _log4net.Info("ProductController Product Details Edited Successfully");
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            _log4net.Info("ProductController Deleteing Product");
            HttpClient client = _api.Initial();
            var delete = client.DeleteAsync("/api/Product?id=" + id);
            var res = delete.Result;
            return RedirectToAction("Index");
        }
    }
}
