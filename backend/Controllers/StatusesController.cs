using DBinit;
using DBinit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/statuses")]
[Authorize]
public class StatusesController : ControllerBase
{
    private readonly AppDbContext _db;
    public StatusesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _db.MeasurementStatuses.ToListAsync());
} 