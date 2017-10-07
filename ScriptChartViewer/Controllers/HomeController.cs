using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScriptChartViewer.Models;
using System.Net.Http;

namespace WebChartViewer.Controllers
{
    public class HomeController : Controller
    {
        // /2gWy0MzimOLR0Qp47Qr
        [HttpGet("{chartId}")]
        public IActionResult Index(string chartId)
        {
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress =
                    new Uri("http://88.99.187.144");
                var resp = httpclient.GetAsync($"/api/linechart/{chartId}").GetAwaiter().GetResult();
                resp.EnsureSuccessStatusCode();

                ViewData["DataRows"] = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            ViewData["TestData"] = chartId;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
