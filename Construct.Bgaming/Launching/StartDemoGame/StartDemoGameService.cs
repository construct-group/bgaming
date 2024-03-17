using Construct.Bgaming.Security;
using Construct.Bgaming.Types;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace Construct.Bgaming.Launching.StartDemoGame;

file record StartDemoRequestJson
{
    [JsonProperty("casino_id")] public string CasinoId { get; init; } = null!;
    [JsonProperty("game")] public string Game { get; init; } = null!;
    [JsonProperty("locale")] public string Locale { get; init; } = null!;
    [JsonProperty("ip")] public string Ip { get; init; } = null!;
    [JsonProperty("client_type")] public string ClientType { get; init; } = null!;
    [JsonProperty("urls")] public RequestUrls RequestUrls { get; init; } = null!;
}

file record StartDemoResponseJson
{
    [JsonProperty("launch_options")] public LaunchOptions LaunchOptions { get; init; } = null!;
}

internal class StartDemoGameService : IStartDemoGameService
{
    private readonly BgamingConfigurationParameters parameters;
    private readonly IBgamingSecurityService securityService;
    private readonly ILogger<StartDemoGameService> logger;

    public StartDemoGameService(
        BgamingConfigurationParameters parameters,
        IBgamingSecurityService securityService,
        ILogger<StartDemoGameService> logger)
    {
        this.securityService = securityService;
        this.parameters = parameters;
        this.logger = logger;
    }

    public async Task<StartDemoResponse> StartDemoAsync(
        StartDemoRequest request,
        CancellationToken cancellationToken = default)
    {
        // TODO: logging
        var restRequest = new StartDemoRequestJson
        {
            CasinoId = parameters.ID,
            Game = request.Game.ToString(),
            Locale = "en",
            Ip = request.Address.ToString(),
            ClientType = request.ClientType.ToString().ToLower(),
            RequestUrls = request.RequestUrls
        };
        using var client = new HttpClient();
        var jsonRequest = JsonConvert.SerializeObject(restRequest);
        if (restRequest.Game.Equals("AcceptanceTest")) jsonRequest = jsonRequest.Replace("AcceptanceTest", "acceptance:test");
        var signature = securityService.GenerateSignature(jsonRequest);
        client.DefaultRequestHeaders.Add("X-REQUEST-SIGN", signature);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var result = await client.PostAsync($"{parameters.URL}/demo", content);
        if (!result.IsSuccessStatusCode)
            throw new HttpRequestException($"Request to provider returned: {((int)result.StatusCode)}");
        var jsonResponse = await result.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<StartDemoResponseJson>(jsonResponse)!;
        return new() { LaunchOptions = response.LaunchOptions };
    }
}
