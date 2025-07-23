using DBinit;
using DBinit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/equipment-possible-types")]
[Authorize]
public class EquipmentPossibleTypesController : ControllerBase
{
    private readonly AppDbContext _db;
    public EquipmentPossibleTypesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _db.EquipmentPossibleTypes.ToListAsync());
} 