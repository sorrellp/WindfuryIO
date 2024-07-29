using GraphQL;
using GraphQL.Client.Http;
using WarcraftLogs.Models;

namespace WarcraftLogs.GraphQL;

public class GetDamageDoneTable
{
    private readonly GraphQLHttpClient _client;

    public GetDamageDoneTable(GraphQLHttpClient client)
    {
        ArgumentNullException.ThrowIfNull(client, nameof(client));

        _client = client;
    }

    public async Task<List<DamageDoneTable>> Execute(string fightCode, int fightId, int sourceId)
    {
        var request = new GraphQLRequest
        {
            Query = @"
            query($fightCode: String, $fightId: Int, $sourceId: Int) {
                reportData {
                    report(code: $fightCode) {
                        table(fightIDs: [$fightId], sourceID: $sourceId, dataType: DamageDone)
                    }
                }
            }",
            Variables = new
            {
                fightCode, 
                fightId, 
                sourceId
            }
        };

        var type = await _client.SendQueryAsync<ReportDataType<DamageDoneTable>>(request);

        return type.Data.ReportData.Report.Table.Data.Entries;
    }
}
