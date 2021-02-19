using System.Text.Json.Serialization;

namespace AuthenticationAPI.Core.DTOs.Accounts
{
    public class ImpersonationRequest
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }
    }
}
