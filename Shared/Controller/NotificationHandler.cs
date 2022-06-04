namespace Shared;

public interface INotificationHandler
{
    void RaiseError(string errorCode);
    bool HasErrors();
    List<string> GetErrors();
    bool HasInsufficientPermissions();
    bool HasObjectNotFound();
}

public class NotificationHandler : INotificationHandler
{
    private readonly List<string> _errors;

    public NotificationHandler()
    {
        _errors = new List<string>();
    }

    public void RaiseError(string errorCode)
    {
        _errors.Add(errorCode);
    }

    public bool HasErrors() => _errors.Count != 0;
    public bool HasInsufficientPermissions() => _errors.Contains(GenericErrorCodes.InsufficientPermissions);
    public bool HasObjectNotFound() => _errors.Contains(GenericErrorCodes.ObjectNotFound);

    public List<string> GetErrors() => _errors;
}