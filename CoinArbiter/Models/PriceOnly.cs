using Newtonsoft.Json;

/// <summary>
/// Model for deserializing a single price response from Cryptocompare.
/// Generated from JSON using QuickType @ https://app.quicktype.io
/// </summary>
namespace CoinArbiter.Models
{

    public partial class PriceOnly
    {
        [JsonProperty("USD")]
        public double Usd { get; set; }
    }

    public partial class PriceOnly
    {
        public static PriceOnly FromJson(string json) => JsonConvert.DeserializeObject<PriceOnly>(json, Converter.Settings);
    }

    public static class SerializePrice
    {
        public static string ToJson(this PriceOnly self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class ConverterPriceOnly
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
