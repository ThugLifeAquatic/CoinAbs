using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoinArbiter.Models;
using System.Net.Http;

namespace CoinArbiter.Controllers
{
    public class HomeController : Controller
    {
        //Default and only view
        public IActionResult Index()
        {
            return View();
        }

        //Retreives an hourly price history from CryptoCompare, passes simplified data to the front-end for charting
        public async Task<IActionResult> GetHistoryChartJSONAsync()
        {
            //List to hold the simplified coin prices
            List<SimpleCoin> priceList = new List<SimpleCoin>();

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://min-api.cryptocompare.com");

                    //Api Query
                    var response = await client.GetAsync($"/data/histohour?fsym=BTC&tsym=USD&limit=19&aggregate=3&e=CCCAGG");
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    //Deserialize the JSON response
                    var rawData = ApiResponse.FromJson(stringResult);
                    if (rawData.Data != null)
                    {
                        //Populate "priceList" with SimpleCoins
                        foreach (var o in rawData.Data)
                        {
                            //Use response data to instantiate a SimpleCoin
                            SimpleCoin coin = new SimpleCoin
                            {
                                Currency = "USD",
                                USD = o.Close
                            };

                            //Add new SimpleCoin to "priceList"
                            priceList.Add(coin);
                        }
                    }
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting requested price history from CryptoCompare: {httpRequestException.Message}");
                }
                //Return "priceList" as JSON to front-end AJAX call
                return Json(priceList);
            }
        }

        //Identical to above, except only returning a single SimpleCoin instead of a list.
        public async Task<IActionResult> GetLivePriceAsync()
        {
            SimpleCoin newCoin = new SimpleCoin();

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://min-api.cryptocompare.com");

                    var response = await client.GetAsync($"/data/price?fsym=BTC&tsyms=USD");
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();

                    var rawData = PriceOnly.FromJson(stringResult);

                    newCoin.Currency = "USD";
                    newCoin.USD = rawData.Usd;
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting requested price history from CryptoCompare: {httpRequestException.Message}");
                }
                return Json(newCoin);
            }
        }
    }
}