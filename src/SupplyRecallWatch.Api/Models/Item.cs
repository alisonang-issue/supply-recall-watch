using System.ComponentModel.DataAnnotations;

namespace SupplyRecallWatch.Api.Models;

public class Item
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = "";

    [MaxLength(100)]
    public string? Sku { get; set; }

    [MaxLength(200)]
    public string? Manufacturer { get; set; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}