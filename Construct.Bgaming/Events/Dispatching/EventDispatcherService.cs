using Construct.Bgaming.Security;
using Construct.Bgaming.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Construct.Bgaming.Events.Dispatching;

internal class EventDispatcherService : IEventDispatcherService
{
    private static readonly string balancePattern = @"\{(?:""user_id"":""[^""]+"",""currency"":""[^""]+"",""game"":""[^""]+"",""game_id"":""[^""]*"",""session_id"":""[^""]*"",""finished"":(?:true|false),""actions"":\[\]\})";

    private readonly ILogger<EventDispatcherService> logger;
    private readonly IBgamingSecurityService bgamingSecurityService;

    public EventDispatcherService(
        ILogger<EventDispatcherService> logger,
        IBgamingSecurityService bgamingSecurityService)
    {
        this.logger = logger;
        this.bgamingSecurityService = bgamingSecurityService;
    }

    public async Task<DispatchingResult> Dispatch(HttpRequest request)
    {
        var requestBody = await ReadBody(request);

        if (this.CheckSignatrue(request, requestBody, out var result) == false) return result!;
        if (this.IsBalance(requestBody, out result)) return result!;
        if (this.IsRollback(requestBody, out result)) return result!;
        return new(BgamingEventType.Actions, null, JObject.Parse(requestBody!));
    }

    private async Task<string> ReadBody(HttpRequest request, CancellationToken cancellationToken = default)
    {
        var content = new StreamContent(request.Body);
        content.Headers.ContentLength = request.ContentLength;
        return await new StreamContent(request.Body).ReadAsStringAsync();
    }

    private bool CheckSignatrue(HttpRequest request, string requestBody, out DispatchingResult? result)
    {
        if (bgamingSecurityService.ValidateSignature(requestBody,
            request.Headers.First(x => x.Key == "X-Request-Sign").Value!) == false)
        {
            result = new(BgamingEventType.Error, Error.InvalidSign);
            return false;
        }

        result = null;
        return true;
    }
    private bool IsBalance(string requestBody, out DispatchingResult? result)
    {
        if (Regex.IsMatch(requestBody!, balancePattern))
        {
            result = new(BgamingEventType.Balance, null, JObject.Parse(requestBody!));
            return true;
        }

        result = null;
        return false;
    }

    private bool IsRollback(string requestBody, out DispatchingResult? result)
    {
        if (requestBody.Contains("rollback"))
        {
            result = new(BgamingEventType.Rollback, null, JObject.Parse(requestBody!));
            return true;
        }

        result = null;
        return false;
    }
}
