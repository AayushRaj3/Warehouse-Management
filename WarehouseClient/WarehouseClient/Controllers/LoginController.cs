using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseClient.Models;

namespace WarehouseClient.Controllers
{
    public class LoginController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(LoginController));
        public IActionResult Login()
        {
            _log4net.Info("LoginController HttpGet Login Page");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserInfo user)
        {
            _log4net.Info("LoginController HttpPost Sending User Data");
            string token;
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("https://localhost:44348/");
                var jsonString = JsonConvert.SerializeObject(user);
                var data = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
                var response = await httpclient.PostAsync("api/Token/LoginDetail", data);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _log4net.Info("LoginController Login Successfull");
                    token = await response.Content.ReadAsStringAsync();
                    TempData["token"] = token;
                    return RedirectToAction("Index", "Product");
                }
            }
            _log4net.Info("LoginController Login Failed");
            return View("Login");
        }
    }
}
