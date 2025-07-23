using DBinit;
using DBinit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/measurement-points")]
[Authorize]
public class MeasurementPointsController : ControllerBase
{
    private readonly AppDbContext _db;
    public MeasurementPointsController(AppDbContext db) => _db = db;

    // GET /api/equipment/{equipmentId}/measurement-points
    [HttpGet("/api/equipment/{equipmentId}/measurement-points")]
    public async Task<IActionResult> GetByEquipment(int equipmentId)
    {
        var items = await _db.MeasurementPoints
            .Include(mp => mp.Equipment)
            .Where(mp => mp.Equipment.Id == equipmentId)
            .ToListAsync();
        return Ok(items);
    }

    // GET /api/measurement-points/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _db.MeasurementPoints
            .Include(mp => mp.Equipment)
            .FirstOrDefaultAsync(mp => mp.Id == id);
        return item == null ? NotFound() : Ok(item);
    }

    // POST /api/measurement-points
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MeasurementPoint model)
    {
        model.UpdatedAt = DateTime.UtcNow;
        _db.MeasurementPoints.Add(model);
        UpdateHierarchyUpdatedAt(model.Equipment);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

    // PUT /api/measurement-points/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] MeasurementPoint model)
    {
        var item = await _db.MeasurementPoints.Include(mp => mp.Equipment).ThenInclude(e => e.EquipmentType).ThenInclude(et => et.Installation).FirstOrDefaultAsync(mp => mp.Id == id);
        if (item == null) return NotFound();
        item.Name = model.Name;
        item.X_0 = model.X_0;
        item.X_n_1 = model.X_n_1;
        item.X_lim = model.X_lim;
        item.V_d = model.V_d;
        item.DateNominal = model.DateNominal;
        item.DateFinal = model.DateFinal;
        item.NextFixDate = model.NextFixDate;
        item.CoordinateX = model.CoordinateX;
        item.CoordinateY = model.CoordinateY;
        item.Equipment = model.Equipment;
        item.MeasurementStatus = model.MeasurementStatus;
        item.UpdatedAt = DateTime.UtcNow;
        UpdateHierarchyUpdatedAt(item.Equipment);
        await _db.SaveChangesAsync();
        return Ok(item);
    }

    // DELETE /api/measurement-points/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.MeasurementPoints.Include(mp => mp.Equipment).ThenInclude(e => e.EquipmentType).ThenInclude(et => et.Installation).FirstOrDefaultAsync(mp => mp.Id == id);
        if (item == null) return NotFound();
        _db.MeasurementPoints.Remove(item);
        UpdateHierarchyUpdatedAt(item.Equipment);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    private void UpdateHierarchyUpdatedAt(Equipment equipment)
    {
        if (equipment == null) return;
        equipment.UpdatedAt = DateTime.UtcNow;
        var eqType = equipment.EquipmentType;
        if (eqType != null)
        {
            eqType.UpdatedAt = DateTime.UtcNow;
            var inst = eqType.Installation;
            if (inst != null)
                inst.UpdatedAt = DateTime.UtcNow;
        }
    }
} 