namespace Construct.Bgaming.Security;

public interface IBgamingSecurityService
{
    public string GenerateSignature(string request);

    public bool ValidateSignature(string request, string signature);
}
