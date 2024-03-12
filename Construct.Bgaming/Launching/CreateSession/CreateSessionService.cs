using Construct.Bgaming.Launching.StartDemoGame;
using Construct.Bgaming.Security;
using Construct.Bgaming.Types;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace Construct.Bgaming.Launching.CreateSession;

file record CreateSessionRequestJson
{
    [JsonProperty("user_id ")] public string UserId { get; init; } = null!;
    [JsonProperty("currency")] public string Currency { get; init; } = null!; //
    [JsonProperty("game")] public string Game { get; init; } = null!;
    [JsonProperty("game_id")] public string GameId { get; init; } = null!;
    [JsonProperty("finished")] public bool Finished { get; init; }
    [JsonProperty("actions")] public RequestActions Actions { get; init; } = null!;
}

internal class CreateSessionService : ICreateSessionService
{
    private readonly BgamingConfigurationParameters parameters;
    private readonly IBgamingSecurityService securityService;
    private readonly ILogger<CreateSessionService> logger;

    public CreateSessionService(
    BgamingConfigurationParameters parameters,
    IBgamingSecurityService securityService,
    ILogger<CreateSessionService> logger)
    {
        this.securityService = securityService;
        this.parameters = parameters;
        this.logger = logger;
    }

    public async Task<CreateSessionResponse> CreateSessionAsync(
        CreateSessionRequest request,
        CancellationToken cancellationToken = default)
    {
        var restRequest = new CreateSessionRequestJson
        {
            UserId = request.UserId.ToString(),
            Currency = request.Currency.ToString(),
            Game = request.Game.ToString(),
            GameId = request.GameId.ToString(),
            Finished = request.Finished,
            Actions = request.Actions
        };
        using var client = new HttpClient();
        var jsonRequest = JsonConvert.SerializeObject(request);

        var signature = securityService.GenerateSignature(jsonRequest);
        client.DefaultRequestHeaders.Add("X-REQUEST-SIGN", signature);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var result = await client.PostAsync($"{parameters.URL}/sessions", content);
        if (!result.IsSuccessStatusCode)
            throw new HttpRequestException($"Request to provider returned: {((int)result.StatusCode)}");
        var jsonResponse = await result.Content.ReadAsStringAsync();
        dynamic response = JsonConvert.DeserializeObject(jsonResponse)!;
        return new()
        {
            Balance = Convert.ToDouble(response["balance"]),
            GameId = response["game_id"],
            Transactions = new()
            {
                ActionId = response["transactions"]["action_id"],
                TxId = response["transactions"]["tx_id"],
                ProcessedAt = (response["transactions"]["processed_at"]).ToString().FromISO8601()
            }
        };
    }
}