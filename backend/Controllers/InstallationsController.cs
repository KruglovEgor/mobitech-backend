using DBinit;
using DBinit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/installations")]
[Authorize]
public class InstallationsController : ControllerBase
{
    private readonly AppDbContext _db;
    public InstallationsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _db.Installations.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _db.Installations.FindAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Installation model)
    {
        model.UpdatedAt = DateTime.UtcNow;
        _db.Installations.Add(model);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Installation model)
    {
        var item = await _db.Installations.FindAsync(id);
        if (item == null) return NotFound();
        item.Name = model.Name;
        item.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Installations.FindAsync(id);
        if (item == null) return NotFound();
        _db.Installations.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }
} 