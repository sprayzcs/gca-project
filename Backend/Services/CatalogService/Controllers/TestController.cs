using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Controller;

namespace CatalogService.Controllers;

[Route("[controller]/[action]")]
public class TestController : BaseController
{
    private readonly INotificationHandler _notificationHandler;

    public TestController(INotificationHandler notificationHandler) : base(notificationHandler)
    {
        _notificationHandler = notificationHandler;
    }

    [HttpGet]
    public IActionResult test()
    {
        _notificationHandler.RaiseError(GenericErrorCodes.ObjectNotFound);
        return Result("hallo");
    }
}