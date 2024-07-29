using WarcraftLogs.GraphQL;
using GraphQL;
using System.Text.Json.Serialization;

namespace WarcraftLogs.Models;

internal record ReportDataType<T>(ReportType<T> ReportData) where T : Table;

internal record ReportType<T>(Report<T> Report) where T : Table;

internal record Report<T>(TableType<T> Table, MasterData MasterData, string Code, int FightId) where T : Table;

internal record TableType<T>(TableData<T> Data) where T : Table;

internal record TableData<T>(List<T> Entries,
    CombatantInfo CombatantInfo) where T : Table;

public record DamageDoneTable(string Name, 
    int GUID,
    int Type,
    int Total, 
    string AbilityIcon,
    int Uses,
    int HitCount,
    int TickCount,
    int CritHitCount,
    List<HitDetails> HitDetails,
    SummaryTable CombatantInfo) : Table;

public record Table();

public record HitDetails(string Type, int Total, int Count, int Min, int Max);

internal record MasterData(List<Actor> Actors);

public record Actor(int Id, string Name, string Type);

internal record WorldDataType(WorldData WorldData);

internal record WorldData(Encounter Encounter);

internal record Encounter(string Name, CharacterRankings CharacterRankings);

internal record CharacterRankings(int Page, bool HasMorePages, int Count,
    List<Ranking> Rankings);

internal record Ranking(string Name, 
    string @Class,
    string Spec, 
    double Amount, 
    int BracketData, 
    Report<Table> Report);

public record SummaryTable(CombatantInfo CombatantInfo,
    int TotalTime,
    int ItemLevel) : Table;

public record CombatantInfo(Stats Stats);

public record Stats(Stat Speed,
    Stat Agility,
    Stat Mastery,
    Stat Crit,
    [property: JsonPropertyName("Item Level")] Stat ItemLevel,
    Stat Stamina, 
    Stat Avoidance, 
    Stat Haste,
    Stat Versatility,
    Stat Leech);

public record Stat(int Min, int Max);