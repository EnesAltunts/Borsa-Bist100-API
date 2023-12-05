using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace VeriCekme_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorsaController : ControllerBase
    {
        [HttpGet]   
        public async Task<IEnumerable<string>> Data()
        {
            // HTML içeriğini almak için HttpClient kullanılıyor
            using (HttpClient client = new HttpClient())
            {
                // Sayfanın HTML içeriğini al
                var htmlContent = await client.GetStringAsync("https://uzmanpara.milliyet.com.tr/canli-borsa/bist-100-hisseleri/");

                // HTML içeriğini işlemek için HtmlWeb ve HtmlNodeCollection kullanılıyor
                var web = new HtmlWeb();
                var doc = new HtmlDocument();
                doc.LoadHtml(htmlContent);

                // HTML içinde belirli bir tabloyu seçmek için XPath kullanılıyor
                var td = doc.DocumentNode.SelectNodes("//table[@class='table3']//tr//td");

                // İşlenmiş verileri bir liste olarak döndür
                if (td != null)
                {
                    return td.Select(item => item.InnerText).ToList();
                }

                return new List<string>(); // Eğer veri bulunamazsa boş liste döndür
            }
        }
    }
}
