namespace Shared.Security;

public interface IIdentityService
{
    string CreateIdentityToken(string forIdentity);
    bool ValidateIdentityToken(string identityToken, out string serviceName);
}
