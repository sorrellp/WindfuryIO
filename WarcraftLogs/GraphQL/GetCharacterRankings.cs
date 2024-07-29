using GraphQL;
using GraphQL.Client.Http;
using WarcraftLogs.Models;

namespace WarcraftLogs.GraphQL;

public class GetCharacterRankings
{
    private readonly GraphQLHttpClient _client;

    public GetCharacterRankings(GraphQLHttpClient client)
    {
        ArgumentNullException.ThrowIfNull(client, nameof(client));

        _client = client;
    }

    public async Task<List<CharacterRanking>> Execute(int encounterId, string className, string specName)
    {
        var requestWorldData = new GraphQLRequest
        {
            Query = @"
            query($encounterId: Int, $className: String, $specName: String) {
                worldData {
                    encounter (id: $encounterId) {
                        name,
                        characterRankings (className: $className, specName: $specName) 
                    }
                }
            }
            ",
            Variables = new
            {
                encounterId,
                className,
                specName
            }
        };

        //todo investigate response type
        var type = await _client.SendQueryAsync<WorldDataType>(requestWorldData);

        return type.Data.WorldData.Encounter.CharacterRankings.Rankings.Take(100)
            .Select(x => new CharacterRanking { Name = x.Name, ReportCode = x.Report.Code, FightId = x.Report.FightId }).ToList();
    }
}
