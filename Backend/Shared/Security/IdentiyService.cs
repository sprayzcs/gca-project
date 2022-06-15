using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Security.Model;

namespace Shared.Security;

// primitive way of doing it, should be replaced by an authenticated service of some extend
public class IdentiyService : IIdentityService
{
    private const string _issuer = "https://authentication-service";
    private const string _claim = "forService";

    private readonly IConfiguration _configuration;
    private readonly SecurityInfoModel _securityInfo;
    private readonly SymmetricSecurityKey _securityKey;

    public IdentiyService(IConfiguration configuration, SecurityInfoModel securityInfo)
    {
        _configuration = configuration;
        _securityInfo = securityInfo;

        _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityInfo.Secret));
    }

    // token does not expire as of now
    public string CreateIdentityToken(string forIdentity)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.MaxValue,
            Issuer = _issuer,
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(_claim, forIdentity)
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
                ValidIssuer = _issuer,
                IssuerSigningKey = _securityKey
            }, out var validatedToken);

            var serviceClaim = claims.FindFirst(_claim);
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
