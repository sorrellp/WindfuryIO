using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WarcraftLogs.Models;
using WindfuryIO.API.Models;

namespace WarcraftLogs.Services;

public class WarcraftLogsAuthorizationService(HttpClient httpClient, IOptions<SecretConstants> secretConstansOptions)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly SecretConstants _secretConstans = secretConstansOptions.Value;
    private const string _grantType = "client_credentials";

    public async Task<TokenResponse> GetToken()
    {
        _httpClient.BaseAddress = new Uri("https://www.warcraftlogs.com/oauth/token");

        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var allIputParams = new List<KeyValuePair<string, string>>
        {
            new("client_id", _secretConstans.ClientId),
            new("client_secret", _secretConstans.ClientSecret),
            new("grant_type", _grantType)
        };

        HttpContent requestParams = new FormUrlEncodedContent(allIputParams);
        var response = await _httpClient.PostAsync("", requestParams);
        //TODO Error handling
        return await response.Content.ReadFromJsonAsync<TokenResponse>();
    }
}
