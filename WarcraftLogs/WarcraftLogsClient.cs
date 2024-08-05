using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using System.Net.Http.Headers;
using System.Text.Json;
using WarcraftLogs.Services;

namespace WarcraftLogs;

public class WarcraftLogsClient(WarcraftLogsAuthorizationService warcraftLogsAuthorizationService)
{
    private readonly WarcraftLogsAuthorizationService _warcraftLogsAuthorizationService = warcraftLogsAuthorizationService;

    public async Task<GraphQLHttpClient> Initialize()
    {
        var tokenResponse = await _warcraftLogsAuthorizationService.GetToken();
        var client = new GraphQLHttpClient("https://www.warcraftlogs.com/api/v2/client", new SystemTextJsonSerializer(
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }
        ));
        client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
        return client;
    }
}
