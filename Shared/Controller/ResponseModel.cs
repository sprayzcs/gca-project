namespace Shared;

public record ResponseModel
{
    public object? Data { get; set; }
    public bool Success { get; set; }
    public List<string>? Error { get; set; }
}