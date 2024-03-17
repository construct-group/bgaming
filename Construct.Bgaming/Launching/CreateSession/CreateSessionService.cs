using Construct.Bgaming.Security;
using Construct.Bgaming.Types;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace Construct.Bgaming.Launching.CreateSession;

file record CreateSessionRequestJson
{
    [JsonProperty("casino_id")] public string CasinoId { get; init; } = null!;
    [JsonProperty("game")] public string Game { get; init; } = null!;
    [JsonProperty("currency")] public string Currency { get; init; } = null!;
    [JsonProperty("locale")] public string Locale { get; init; } = null!;
    [JsonProperty("ip")] public string Ip { get; init; } = null!;
    [JsonProperty("balance")] public long Balance { get; init; }
    [JsonProperty("client_type")] public string ClientType { get; init; } = null!;
    [JsonProperty("urls")] public RequestUrls RequestUrls { get; init; } = null!;
    [JsonProperty("user")] public UserJson User { get; init; } = null!;
}

file record UserJson
{
    [JsonProperty("id")] public string Id { get; init; } = null!;
    [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)] public string? Email { get; set; }
    [JsonProperty("firstname")] public string FirstName { get; init; } = null!;
    [JsonProperty("lastname")] public string LastName { get; init; } = null!;
    [JsonProperty("nickname")] public string Nickname { get; init; } = null!;
    [JsonProperty("city")] public string City { get; init; } = null!;
    [JsonProperty("country")] public string Country { get; init; } = null!;
    [JsonProperty("date_of_birth")] public string DateOfBirth { get; init; } = null!;
    [JsonProperty("gender")] public string Gender { get; init; } = null!;
    [JsonProperty("registered_at")] public string RegisteredAt { get; init; } = null!;
}

file record CreateSessionResponseJson
{
    [JsonProperty("launch_options")] public LaunchOptions LaunchOptions { get; init; } = null!;
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
        // TODO: logging
        var restRequest = new CreateSessionRequestJson
        {
            CasinoId = parameters.ID,
            Game = request.Game.ToString(),
            Currency = request.Money.Code,
            Locale = "en",
            Ip = request.IPAddress.ToString(),
            Balance = request.Money.Amount,
            ClientType = request.ClientType.ToString().ToLower(),
            RequestUrls = request.RequestUrls,
            User = new()
            {
                Id = request.User.Id.ToString(),
                FirstName = request.User.FirstName,
                LastName = request.User.LastName,
                Nickname = request.User.Nickname,
                City = request.User.City,
                Country = request.User.Country,
                DateOfBirth = request.User.DateOfBirth.ToISO8601Date(),
                Gender = request.User.Gender.ToString().First().ToString().ToLower(),
                RegisteredAt = request.User.DateOfRegistration.ToISO8601Date()
            }
        };
        if (request.User.Email is not null) restRequest.User.Email = request.User.Email;
        using var client = new HttpClient();
        var jsonRequest = JsonConvert.SerializeObject(restRequest);
        if (restRequest.Game.Equals("AcceptanceTest")) jsonRequest = jsonRequest.Replace("AcceptanceTest", "acceptance:test");
        var signature = securityService.GenerateSignature(jsonRequest);
        client.DefaultRequestHeaders.Add("X-REQUEST-SIGN", signature);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var result = await client.PostAsync($"{parameters.URL}/sessions", content);
        if (!result.IsSuccessStatusCode)
            throw new HttpRequestException($"Request to provider returned: {((int)result.StatusCode)}");
        var jsonResponse = await result.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<CreateSessionResponseJson>(jsonResponse)!;
        return new() { LaunchOptions = response.LaunchOptions };
    }
}