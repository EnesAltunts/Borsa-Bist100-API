using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Borsa_BIST100.Controllers
{
    public class BorsaController : Controller
    {
        public async Task<ActionResult> Index()
        {
            // API'den veri çekme
            var apiUrl = "https://localhost:44386/api/borsa";

            using (var httpClient = new HttpClient())
            {
                string response = await httpClient.GetStringAsync(apiUrl);


                // JObject json = JObject.Parse(response);

                JArray arr = JArray.Parse(response);

                List<string> hisseList = new List<string>();
                List<string> fiyatList = new List<string>();
                List<string> degisimList = new List<string>();
                List<string> saatDeger = new List<string>();

                for (int i = 0; i < arr.Count; i += 5)
                {
                    hisseList.Add(arr[i].ToString());
                    fiyatList.Add(arr[i + 2].ToString());
                    degisimList.Add(arr[i + 3].ToString());
                    saatDeger.Add(arr[i + 4].ToString());
                }

                ViewBag.HisseList = hisseList;
                ViewBag.FiyatList = fiyatList;
                ViewBag.DegisimList = degisimList;
                ViewBag.SaatDeger = saatDeger;

                return View();
            }
        }
    }
}
