using System.Text.Json.Serialization;

namespace AuthenticationAPI.Core.DTOs.Accounts
{
    public class RefreshTokenRequest
    {
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
