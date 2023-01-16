using GoldUndSilber.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Net.Http;
using System.Text.Json.Nodes;

namespace GoldUndSilber.Controllers
{
    public class GoldController : Controller
    {
        public IActionResult Index()
        {
            var model = new GoldModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult GetGoldPrice(GoldModel goldModel)
        {
            double price = 0;
            double unzeToGramm = 28.3495;

            using (var client = new HttpClient())
            {
                var json = client.GetStringAsync("https://api.edelmetalle.de/public.json").Result;
                var goldData = JsonNode.Parse(json);
                double goldPrice = ((double)goldData["gold_eur"]) / unzeToGramm;
                //var silverData = JsonSerializer.Deserialize<dynamic>(json);
                price = goldPrice * goldModel.Grams;
            }
            goldModel.Price = Math.Round(price,2);
            return View("Index", goldModel);
        }

    }
}
