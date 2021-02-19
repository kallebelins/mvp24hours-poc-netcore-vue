using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AuthenticationAPI.Core.DTOs.Accounts
{
    public class LoginResult
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("claims")]
        public IEnumerable<ClaimResult> Claims { get; set; }

        [JsonPropertyName("originalUserName")]
        public string OriginalUserName { get; set; }

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }

}
