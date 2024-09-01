using System.Text.Json.Serialization;

namespace Payment.Proccessor.Models;

public class JsonModel : JsonRequest
{
    [JsonPropertyName("public_key")]
    public string PublicKey { get; set; } = "sandbox_i57108353826";

    [JsonPropertyName("private_key")]
    public string PrivateKey { get; set; } = "sandbox_S7ubZnIOYz54ZXF5ctR4wc5zAsb8vJzwVz8UrOZM";
}
