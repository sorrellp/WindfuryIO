// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using WarcraftLogs;
using WarcraftLogs.GraphQL;
using WarcraftLogs.Models;


async Task<Dictionary<string, Output>> GetRankings(int boss)
{
    try
    {
        var client = await WarcraftLogsClient.Initialize();

        //Get Character Rankings
        var rankings = await new GetCharacterRankings(client).Execute(boss, "Shaman", "Enhancement");

        var output = new Dictionary<string, Output>();

        //Loop Character Data
        foreach (var ranking in rankings)
        {
            //Get Report Data
            var report = await new GetReportData(client).Execute(ranking.ReportCode);
            if (report == null || report.Count == 0)
                continue;
            var actor = report.First(x => x.Name == ranking.Name);
            //Get Table
            var damageDoneTable = await new GetDamageDoneTable(client)
                .Execute(ranking.ReportCode, ranking.FightId, actor.Id);

            var combatantInfo = await new GetCombatantInfo(client)
                .Execute(ranking.ReportCode, ranking.FightId, actor.Id);

            //Add to List
            output.Add(actor.Name, new Output(damageDoneTable, combatantInfo));
            Console.WriteLine($"Processed {ranking.Name}");
        }

        return output;
    }
    catch(Exception ex)
    {
        Console.WriteLine($"{boss} {ex}");
    }

    return [];
}

try
{
    var properties = typeof(WarcraftLogsConstants.Encounters.Amirdrassil).GetFields();
    var values = properties.Select(x => (int)x.GetValue(null));

    foreach (var boss in values)
    {
        var output = await GetRankings(boss);

        var jsonOutput = JsonSerializer.Serialize(output);
        File.WriteAllText($"{boss}-output.json", jsonOutput);
    }
}
catch(Exception ex)
{

}

record Output(List<DamageDoneTable> DamageDone, CombatantInfo CombatantInfo);