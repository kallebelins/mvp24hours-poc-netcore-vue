using System.Text.Json.Serialization;

namespace AuthenticationAPI.Core.DTOs.Accounts
{
    public class ClaimResult
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
