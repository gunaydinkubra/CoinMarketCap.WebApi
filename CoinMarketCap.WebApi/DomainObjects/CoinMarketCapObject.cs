using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinMarketCap.WebApi.DomainObjects
{
    public class CoinMarketCapObject
    {
        [JsonProperty(PropertyName = "status")]
        public CoinMarketCapStatusObject Status { get; set; }

        [JsonProperty(PropertyName = "data")]
        public List<CoinMarketCapDataObject> Data { get; set; }
    }
    public class CoinMarketCapStatusObject
    {
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty(PropertyName = "error_code")]
        public bool ErrorCode { get; set; }

        [JsonProperty(PropertyName = "error_message")]
        public string ErrorMessage { get; set; }

        [JsonProperty(PropertyName = "elapsed")]
        public int Elapsed { get; set; }

        [JsonProperty(PropertyName = "credit_count")]
        public int CreditCount { get; set; }

        [JsonProperty(PropertyName = "notice")]
        public string Notice { get; set; }

        [JsonProperty(PropertyName = "total_count")]
        public int TotalCount { get; set; }
    }

    public class CoinMarketCapDataObject
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }

        [JsonProperty(PropertyName = "last_updated")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty(PropertyName = "quote")]
        public CoinMarketCapQuoteObject Quote { get; set; }
    }

    public class CoinMarketCapQuoteObject
    {
        [JsonProperty(PropertyName = "USD")]
        public CoinMarketCapUSDObject USD { get; set; }
    }

    public class CoinMarketCapUSDObject
    {
        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }

        [JsonProperty(PropertyName = "volume_24h")]
        public double Volume24h { get; set; }

        [JsonProperty(PropertyName = "percent_change_1h")]
        public double PercentChange1h { get; set; }

        [JsonProperty(PropertyName = "percent_change_24h")]
        public double PercentChange24h { get; set; }

        [JsonProperty(PropertyName = "percent_change_7d")]
        public double PercentChange7d { get; set; }

        [JsonProperty(PropertyName = "market_cap")]
        public double MarketCap { get; set; }

        [JsonProperty(PropertyName = "last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}
