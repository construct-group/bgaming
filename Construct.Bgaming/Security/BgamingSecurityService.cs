using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace Construct.Bgaming.Security;

internal class BgamingSecurityService : IBgamingSecurityService
{
    private readonly ILogger<BgamingSecurityService> logger;
    private readonly BgamingConfigurationParameters parameters;

    public BgamingSecurityService(
        ILogger<BgamingSecurityService> logger,
        BgamingConfigurationParameters parameters)
    {
        this.logger = logger;
        this.parameters = parameters;
    }

    public string GenerateSignature(string request)
    {
        logger.LogDebug("{0}: generating signature for char[{1}]", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), request.Length);
        var hash = new HMACSHA256(Encoding.UTF8.GetBytes(parameters.Token));
        var signature = hash.ComputeHash(Encoding.UTF8.GetBytes(request));
        return Convert.ToBase64String(signature);
    }

    public bool ValidateSignature(string request, string signature)
    {
        var generatedSignature = this.GenerateSignature(request);
        return generatedSignature.Equals(signature);
    }
}
