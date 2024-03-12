namespace Construct.Bgaming.Security;

internal interface IBgamingSecurityService
{
    public string GenerateSignature(string request);

    public bool ValidateSignature(string request, string signature);
}
