using Newtonsoft.Json;

/// <summary>
/// Model for deserializing a list of coin prices from Cryptocompare.
/// Generated from JSON using QuickType @ https://app.quicktype.io
/// </summary>
namespace CoinArbiter.Models
{
    public partial class ApiResponse
    {
        [JsonProperty("Response")]
        public string Response { get; set; }

        [JsonProperty("Type")]
        public long Type { get; set; }

        [JsonProperty("Aggregated")]
        public bool Aggregated { get; set; }

        [JsonProperty("Data")]
        public Datum[] Data { get; set; }

        [JsonProperty("TimeTo")]
        public long TimeTo { get; set; }

        [JsonProperty("TimeFrom")]
        public long TimeFrom { get; set; }

        [JsonProperty("FirstValueInArray")]
        public bool FirstValueInArray { get; set; }

        [JsonProperty("ConversionType")]
        public ConversionType ConversionType { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("close")]
        public double Close { get; set; }

        [JsonProperty("high")]
        public double High { get; set; }

        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("open")]
        public double Open { get; set; }

        [JsonProperty("volumefrom")]
        public double Volumefrom { get; set; }

        [JsonProperty("volumeto")]
        public double Volumeto { get; set; }
    }

    public partial class ConversionType
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("conversionSymbol")]
        public string ConversionSymbol { get; set; }
    }

    public partial class ApiResponse
    {
        public static ApiResponse FromJson(string json) => JsonConvert.DeserializeObject<ApiResponse>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ApiResponse self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
