using Newtonsoft.Json;

namespace Construct.Bgaming.Types;
public record Error
{
    private Error(int code, string message)
    {
        Code = code;
        Message = message;
    }

    [JsonProperty("code")] public int Code { get; init; }
    [JsonProperty("message")] public string Message { get; init; } = null!;


    public static readonly Error InvalidSign = new(403, "Request sign doesn't match");
    public static readonly Error BadRequest = new(400, "Badly formatted JSON");
    public static readonly Error NoFunds = new (100, "Not enough funds");

    public string Serialize() => JsonConvert.SerializeObject(this);
}
