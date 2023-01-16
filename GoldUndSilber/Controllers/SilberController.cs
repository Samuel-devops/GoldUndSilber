using GoldUndSilber.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace GoldUndSilber.Controllers
{
    public class SilberController : Controller
    {
        public IActionResult Index()
        {
            var model = new SilberModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult GetSilverPrice(SilberModel silverModel)
        {
            double price = 0;
            double unzeToGramm = 28.3495;

            using (var client = new HttpClient())
            {
                var json = client.GetStringAsync("https://api.edelmetalle.de/public.json").Result;
                var silberData = JsonNode.Parse(json);
                double silverPrice = ((double)silberData["silber_eur"]) / unzeToGramm;
                //var silverData = JsonSerializer.Deserialize<dynamic>(json);
                price = silverPrice * silverModel.Grams;
            }
            silverModel.Price = Math.Round(price,2);
            return View("Index", silverModel);
        }
    }
}