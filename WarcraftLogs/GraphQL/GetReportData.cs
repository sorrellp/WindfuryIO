using GraphQL;
using GraphQL.Client.Http;
using WarcraftLogs.Models;

namespace WarcraftLogs.GraphQL;

public class GetReportData
{
    private readonly GraphQLHttpClient _client;

    public GetReportData(GraphQLHttpClient client)
    {
        ArgumentNullException.ThrowIfNull(client, nameof(client));

        _client = client;
    }

    public async Task<List<Actor>> Execute(string code)
    {
        var requestActors = new GraphQLRequest
        {
            Query = @"
                 query($code: String) {
                     reportData {
                         report(code: $code) {
                             masterData {
                                 actors(type: ""Player"", subType: ""Shaman"") {
                                     id,
                                     name,
                                     type
                                 }
                             }
                         }
                     }
                 }
                ",
            Variables = new
            {
                code
            }
        };

         var actorResponse = await _client.SendQueryAsync<ReportDataType<Table>>(requestActors);

        return actorResponse.Data.ReportData.Report.MasterData.Actors;
    }
}