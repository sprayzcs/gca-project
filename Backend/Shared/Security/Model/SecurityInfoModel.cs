namespace Shared.Security.Model;

public record SecurityInfoModel
{
    public string Secret { get; init; } = string.Empty;
    public List<string> ValidAccessors { get; init; } = new();
    public string Self { get; init; } = string.Empty;
}
