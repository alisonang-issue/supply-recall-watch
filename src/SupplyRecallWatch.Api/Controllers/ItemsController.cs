using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupplyRecallWatch.Api.Data;
using SupplyRecallWatch.Api.Models;

namespace SupplyRecallWatch.Api.Controllers;

[ApiController]
[Route("api/[controller]")] //turns the class name into the url
public class ItemsController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetAll()
    {
        return await db.Items.AsNoTracking().OrderBy(i => i.Name).ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Item>> GetById(int id)
    {
        var item = await db.Items.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<Item>> Create(Item item)
    {
        item.Id = 0;
        item.CreatedUtc = DateTime.UtcNow;
        db.Items.Add(item);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Replace(int id, Item update)
    {
        var item = await db.Items.FindAsync(id);
        if (item is null) return NotFound();

        item.Name = update.Name;
        item.Sku = update.Sku;
        item.Manufacturer = update.Manufacturer;
        item.Quantity = update.Quantity;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Patch(int id, ItemPatch patch)
    {
        var item = await db.Items.FindAsync(id);
        if (item is null) return NotFound();

        if (patch.Name is not null) item.Name = patch.Name;
        if (patch.Quantity is not null) item.Quantity = patch.Quantity.Value;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await db.Items.FindAsync(id);
        if (item is null) return NotFound();

        db.Items.Remove(item);
        await db.SaveChangesAsync();
        return NoContent();
    }
}