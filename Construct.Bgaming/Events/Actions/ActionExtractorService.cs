using Construct.Bgaming.Events.Dispatching;
using Construct.Bgaming.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace Construct.Bgaming.Events.Actions;

file record BgamingActionJson
{
    [JsonProperty("action_id")] public string Id { get; init; }
    [JsonProperty("action")] public string Action { get; init; } = null!;
    [JsonProperty("amount")] public long Amount { get; init; }
}

file record BgamingActionRequestJson
{
    [JsonProperty("user_id")] public Guid UserId { get; init; }
    [JsonProperty("game_id")] public Guid GameId { get; init; }
    [JsonProperty("session_id")] public Guid SessionId { get; init; }
    [JsonProperty("currency")] public string Currency { get; init; } = null!;
    [JsonProperty("game")] public string Game { get; init; } = null!;
    [JsonProperty("finished")] public bool Finished { get; init; }
    [JsonProperty("actions")] public IEnumerable<BgamingActionJson> Actions { get; init; } = null!;
}

file record BgamingRollbackJson
{
    [JsonProperty("action")] public string Action { get; init; } = null!;
    [JsonProperty("action_id")] public string Id { get; init; } = null!;
    [JsonProperty("original_action_id")] public string OriginalId { get; init; } = null!;
}

file record BgamingRollbackRequestJson
{
    [JsonProperty("user_id")] public Guid UserId { get; init; }
    [JsonProperty("game_id")] public Guid GameId { get; init; }
    [JsonProperty("session_id")] public Guid SessionId { get; init; }
    [JsonProperty("currency")] public string Currency { get; init; } = null!;
    [JsonProperty("game")] public string Game { get; init; } = null!;
    [JsonProperty("finished")] public bool Finished { get; init; }
    [JsonProperty("actions")] public IEnumerable<BgamingRollbackJson> Actions { get; init; } = null!;
}

internal class ActionExtractorService : IActionExtractorService
{
    private readonly ILogger<ActionExtractorService> logger;

    public ActionExtractorService(
        ILogger<ActionExtractorService> logger)
    {
        this.logger = logger;
    }

    public BgamingActionRequest ExtractActions(
        DispatchingResult result,
        CancellationToken cancellationToken = default)
    {
        if (Guid.TryParse(result.Object["game_id"].ToString(), out var _) == false) result.Object["game_id"] = Guid.NewGuid();
        if (Guid.TryParse(result.Object["session_id"].ToString(), out var _) == false) result.Object["session_id"] = Guid.NewGuid();
        var requestJsonObject = result.Object!.ToObject<BgamingActionRequestJson>();
        if (requestJsonObject == null) throw new ArgumentNullException();
        return new()
        {
            UserId = requestJsonObject.UserId,
            GameId = requestJsonObject.GameId,
            SessionId = requestJsonObject.SessionId,
            Game = Enum.Parse<Game>(requestJsonObject.Game.Replace("acceptance:test", "AcceptanceTest")),
            Finished = requestJsonObject.Finished,
            Currency = requestJsonObject.Currency,
            Actions = requestJsonObject.Actions.Select(x => new BgamingAction()
            {
                Id = x.Id,
                Action = x.Action switch
                {
                    "bet" => BgamingActionType.Bet,
                    "win" => BgamingActionType.Win,
                    _ => throw new ArgumentException(),
                },
                Amount = new Currency(x.Amount, requestJsonObject.Currency)
            })
        };
    }

    public BgamingRollbackRequest ExtractRollback(
        DispatchingResult result,
        CancellationToken cancellationToken = default)
    {
        if (Guid.TryParse(result.Object["game_id"].ToString(), out var _) == false) result.Object["game_id"] = Guid.NewGuid();
        var requestJsonObject = result.Object!.ToObject<BgamingRollbackRequestJson>();
        if (requestJsonObject == null) throw new ArgumentNullException();
        return new()
        {
            UserId = requestJsonObject.UserId,
            GameId = requestJsonObject.GameId,
            SessionId = requestJsonObject.SessionId,
            Game = Enum.Parse<Game>(requestJsonObject.Game.Replace("acceptance:test", "AcceptanceTest")),
            Finished = requestJsonObject.Finished,
            Currency = requestJsonObject.Currency,
            Actions = requestJsonObject.Actions.Select(x => new BgamingRollback()
            {
                Id = x.Id,
                OriginalId = x.OriginalId,
                Action = BgamingActionType.Rollback
            })
        };
    }
}
