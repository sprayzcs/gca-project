namespace Shared;

public record ResponseModel<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public List<string>? Error { get; set; }
}

public record ResponseModel : ResponseModel<object> { }
