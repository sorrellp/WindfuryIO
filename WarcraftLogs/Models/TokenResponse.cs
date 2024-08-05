using System.Text.Json.Serialization;

namespace WarcraftLogs.Models;

public record TokenResponse(
[property: JsonPropertyName("token_type")] string TokenType,
[property: JsonPropertyName("expires_in")] int ExpiresIn,
[property: JsonPropertyName("access_token")] string AccessToken);
