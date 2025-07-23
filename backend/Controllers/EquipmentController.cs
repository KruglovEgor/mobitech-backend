using DBinit;
using DBinit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/equipment")]
[Authorize]
public class EquipmentController : ControllerBase
{
    private readonly AppDbContext _db;
    public EquipmentController(AppDbContext db) => _db = db;

    // GET /api/installations/{installationId}/equipment
    [HttpGet("/api/installations/{installationId}/equipment")]
    public async Task<IActionResult> GetByInstallation(int installationId)
    {
        var items = await _db.Equipments
            .Include(e => e.EquipmentType)
            .Where(e => e.EquipmentType.Installation.Id == installationId)
            .ToListAsync();
        return Ok(items);
    }

    // GET /api/equipment/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _db.Equipments
            .Include(e => e.EquipmentType)
            .FirstOrDefaultAsync(e => e.Id == id);
        return item == null ? NotFound() : Ok(item);
    }

    // POST /api/equipment
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Equipment model)
    {
        model.UpdatedAt = DateTime.UtcNow;
        _db.Equipments.Add(model);
        UpdateHierarchyUpdatedAt(model.EquipmentType);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

    // PUT /api/equipment/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Equipment model)
    {
        var item = await _db.Equipments.Include(e => e.EquipmentType).ThenInclude(et => et.Installation).FirstOrDefaultAsync(e => e.Id == id);
        if (item == null) return NotFound();
        item.Name = model.Name;
        item.Percent = model.Percent;
        item.Schema = model.Schema;
        item.EquipmentType = model.EquipmentType;
        item.Contactor = model.Contactor;
        item.UpdatedAt = DateTime.UtcNow;
        UpdateHierarchyUpdatedAt(item.EquipmentType);
        await _db.SaveChangesAsync();
        return Ok(item);
    }

    // DELETE /api/equipment/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Equipments.Include(e => e.EquipmentType).ThenInclude(et => et.Installation).FirstOrDefaultAsync(e => e.Id == id);
        if (item == null) return NotFound();
        _db.Equipments.Remove(item);
        UpdateHierarchyUpdatedAt(item.EquipmentType);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    private void UpdateHierarchyUpdatedAt(EquipmentType eqType)
    {
        if (eqType == null) return;
        eqType.UpdatedAt = DateTime.UtcNow;
        var inst = eqType.Installation;
        if (inst != null)
            inst.UpdatedAt = DateTime.UtcNow;
    }
} 