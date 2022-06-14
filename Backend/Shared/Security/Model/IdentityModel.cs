namespace Shared.Security.Model;

public record IdentityModel
{
    public bool Authenticated { get; set; }
    public string ServiceName { get; set; } = string.Empty;
}
