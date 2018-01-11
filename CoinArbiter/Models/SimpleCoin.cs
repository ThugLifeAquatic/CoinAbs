
/// <summary>
/// A simple coin model that can be instantiated using data from CryptoCompare.
/// Since I only care about the price and currency, this is all the data that I need to start charting prices
/// </summary>
namespace CoinArbiter.Models
{
    public class SimpleCoin
    {
        public string Currency { get; set; }
        public double USD { get; set; }
    }
}
