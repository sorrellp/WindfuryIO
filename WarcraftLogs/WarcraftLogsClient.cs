using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WarcraftLogs;

public class WarcraftLogsClient
{
    public static async Task<GraphQLHttpClient> Initialize()
    {
        var token = await WarcraftLogsAuthorization.GetAuthorizationToken();
        var client = new GraphQLHttpClient("https://www.warcraftlogs.com/api/v2/client", new SystemTextJsonSerializer(
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }
        ));
        client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
        return client;
    }
}
