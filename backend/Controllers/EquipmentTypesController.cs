using DBinit;
using DBinit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/equipment-types")]
[Authorize]
public class EquipmentTypesController : ControllerBase
{
    private readonly AppDbContext _db;
    public EquipmentTypesController(AppDbContext db) => _db = db;

    // GET /api/installations/{installationId}/equipment-types
    [HttpGet("/api/installations/{installationId}/equipment-types")]
    public async Task<IActionResult> GetByInstallation(int installationId)
    {
        var items = await _db.EquipmentTypes
            .Include(et => et.Installation)
            .Include(et => et.EquipmentPossibleType)
            .Where(et => et.Installation.Id == installationId)
            .ToListAsync();
        return Ok(items);
    }

    // GET /api/equipment-types/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _db.EquipmentTypes
            .Include(et => et.Installation)
            .Include(et => et.EquipmentPossibleType)
            .FirstOrDefaultAsync(et => et.Id == id);
        return item == null ? NotFound() : Ok(item);
    }

    // POST /api/equipment-types
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EquipmentType model)
    {
        model.UpdatedAt = DateTime.UtcNow;
        _db.EquipmentTypes.Add(model);
        UpdateHierarchyUpdatedAt(model);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

    // PUT /api/equipment-types/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] EquipmentType model)
    {
        var item = await _db.EquipmentTypes.Include(et => et.Installation).FirstOrDefaultAsync(et => et.Id == id);
        if (item == null) return NotFound();
        item.Percent = model.Percent;
        item.Installation = model.Installation;
        item.EquipmentPossibleType = model.EquipmentPossibleType;
        item.UpdatedAt = DateTime.UtcNow;
        UpdateHierarchyUpdatedAt(item);
        await _db.SaveChangesAsync();
        return Ok(item);
    }

    // DELETE /api/equipment-types/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.EquipmentTypes.Include(et => et.Installation).FirstOrDefaultAsync(et => et.Id == id);
        if (item == null) return NotFound();
        _db.EquipmentTypes.Remove(item);
        UpdateHierarchyUpdatedAt(item);
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