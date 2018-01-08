using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoinArbiter.Models;
using System.Net.Http;

namespace CoinArbiter.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetHistoryChartJSONAsync()
        {
            List<SimpleCoin> priceList = new List<SimpleCoin>();
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://min-api.cryptocompare.com");

                    var response = await client.GetAsync($"/data/histohour?fsym=BTC&tsym=USD&limit=19&aggregate=3&e=CCCAGG");
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    //Deserialize the JSON response
                    var rawData = ApiResponse.FromJson(stringResult);
                    if (rawData.Data != null)
                    {

                        IEnumerable<Datum> coinData = from o in rawData.Data
                                                      where o.Close != 0
                                                      select o;


                        foreach (var o in coinData)
                        {
                            SimpleCoin coin = new SimpleCoin
                            {
                                Currency = "USD",
                                USD = o.Close
                            };
                            priceList.Add(coin);
                        }
                    }
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting requested price history from CryptoCompare: {httpRequestException.Message}");
                }


                return Json(priceList);
            }
        }
    }
}