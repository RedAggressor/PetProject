using System.Text.Json.Serialization;

namespace Payment.Proccessor.Models
{
    public class JsonRequest
    {
        [JsonPropertyName("version")]
        public int Version { get; set; }
        [JsonPropertyName("action")]
        public string Action { get; set; } = null!;
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = null!;
        [JsonPropertyName("description")]
        public string Description { get; set; } = null!;
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = null!;
    }
}
