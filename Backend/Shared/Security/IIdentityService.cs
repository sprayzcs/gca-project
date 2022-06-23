namespace Shared.Security;

public interface IIdentityService
{
    string CreateIdentityToken();
    bool ValidateIdentityToken(string identityToken, out string serviceName);
}
