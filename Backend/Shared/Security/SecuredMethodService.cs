using Shared.Security.Model;

namespace Shared.Security;

public class SecuredMethodService : ISecuredMethodService
{
    private readonly IdentityModel _identityModel;

    public SecuredMethodService(IdentityModel identityModel)
    {
        _identityModel = identityModel;
    }

    public bool CanAccess() => _identityModel.Authenticated;
}
