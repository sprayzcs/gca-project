using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Security.Model;

namespace Shared.Security;

// primitive way of doing it, should be replaced by an authenticated service of some extend
public class IdentityService : IIdentityService
{
    private const string Issuer = "https://authentication-service";
    private const string Claim = "forService";

    private readonly SecurityInfoModel _securityInfo;
    private readonly SymmetricSecurityKey _securityKey;

    public IdentityService(SecurityInfoModel securityInfo)
    {
        _securityInfo = securityInfo;
        _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityInfo.Secret));
    }

    // token does not expire as of now
    public string CreateIdentityToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.MaxValue,
            Issuer = Issuer,
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(Claim, _securityInfo.Self)
            }),
            SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool ValidateIdentityToken(string identityToken, out string serviceName)
    {
        if (identityToken.StartsWith("Bearer"))
        {
            identityToken = identityToken[7..];
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var claims = tokenHandler.ValidateToken(identityToken, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidIssuer = Issuer,
                IssuerSigningKey = _securityKey
            }, out var validatedToken);

            var serviceClaim = claims.FindFirst(Claim);
            if (serviceClaim == null)
            {
                serviceName = string.Empty;
                return false;
            }

            serviceName = serviceClaim.Value;
            return _securityInfo.ValidAccessors.Any(accessor => accessor == serviceClaim.Value) || _securityInfo.Self == serviceName;
        }
        catch
        {
            serviceName = string.Empty;
            return false;
        }
    }
}
