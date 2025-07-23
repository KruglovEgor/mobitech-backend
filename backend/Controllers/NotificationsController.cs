using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/notifications")]
[Authorize]
public class NotificationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        // TODO: получить уведомления пользователя
        return Ok(new[] { new { id = 1, message = "Пример уведомления" } });
    }

    [HttpPost("send")]
    public IActionResult Send([FromBody] NotificationRequest req)
    {
        // TODO: отправить уведомление
        return Ok(new { status = "sent", req.Message });
    }
}

public class NotificationRequest
{
    public string Message { get; set; }
} 