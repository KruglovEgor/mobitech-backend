using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/sync")]
[Authorize]
public class SyncController : ControllerBase
{
    [HttpPost("upload")]
    public IActionResult Upload([FromBody] object data)
    {
        // TODO: обработка синхронизации
        return Ok(new { status = "uploaded" });
    }

    [HttpGet("download")]
    public IActionResult Download([FromQuery] string? lastSync)
    {
        // TODO: выгрузка актуальных данных
        return Ok(new { status = "downloaded", lastSync });
    }
} 