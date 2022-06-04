using Microsoft.AspNetCore.Mvc;

namespace Shared.Controller;

[Controller]
public class BaseController : ControllerBase
{
    private readonly INotificationHandler _notificationHandler;

    public BaseController(INotificationHandler notificationHandler)
    {
        _notificationHandler = notificationHandler;
    }
    
    public IActionResult Result(object? @object)
    {
        if (_notificationHandler.HasInsufficientPermissions())
        {
            return Forbid();
        }

        if (_notificationHandler.HasObjectNotFound())
        {
            return NotFound(new ResponseModel
            {
                Success = false,
                Error = _notificationHandler.GetErrors()
            });
        }

        if (_notificationHandler.HasErrors())
        {
            return BadRequest(new ResponseModel
            {
                Success = false,
                Error = _notificationHandler.GetErrors()
            });
        }

        return Ok(new ResponseModel
        {
            Success = true,
            Data = @object
        });
    }
}
