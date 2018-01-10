using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
