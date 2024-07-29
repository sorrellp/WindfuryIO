using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace WarcraftLogs;

internal class WarcraftLogsAuthorization
{
    private const string _clientId = "93972c4c-5d01-41e4-af81-1352c3e702ce";
    private const string _clientSecret = "KRPqmkCqbQU6VRIKIlS9R4MhKSQ6y6sAfESdMqyv";
    private const string _grantType = "client_credentials";

    internal static async Task<TokenResponse> GetAuthorizationToken()
    {
        var httpClient = new HttpClient();

        httpClient.BaseAddress = new Uri("https://www.warcraftlogs.com/oauth/token");

        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var allIputParams = new List<KeyValuePair<string, string>>
        {
            new("client_id", _clientId),
            new("client_secret", _clientSecret),
            new("grant_type", _grantType)
        };

        HttpContent requestParams = new FormUrlEncodedContent(allIputParams);
        var response = await httpClient.PostAsync("", requestParams);
        //TODO Error handling
        return await response.Content.ReadFromJsonAsync<TokenResponse>();
    }
}

public record TokenResponse(
[property: JsonPropertyName("token_type")] string TokenType,
[property: JsonPropertyName("expires_in")] int ExpiresIn,
[property: JsonPropertyName("access_token")] string AccessToken);
