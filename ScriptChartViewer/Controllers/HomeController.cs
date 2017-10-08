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
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ChartIdModel chartIdModel)
        {
            if (ModelState.IsValid)
            {
                return ChartViewById(chartIdModel.ChartId);
            }
            return View();
        }

        // /2gWy0MzimOLR0Qp47Qr
        [HttpGet("{chartId}")]
        public IActionResult ChartViewById(string chartId)
        {
            ViewData["DataRows"] = QueryDataByChartId(chartId).GetAwaiter().GetResult();
            ViewData["TestData"] = chartId;
            return View("ChartViewById");
        }

        private async Task<string> QueryDataByChartId(string chartId)
        {
            string url = "http://88.99.187.144";
#if DEBUG
            url = "http://localhost:21702";
#endif
            string result;
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress =
                    new Uri(url);
                var resp = httpclient.GetAsync($"/api/linechart/{chartId}").GetAwaiter().GetResult();
                resp.EnsureSuccessStatusCode();

                result = await resp.Content.ReadAsStringAsync();
            }

            return result;
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
