using System.ComponentModel.DataAnnotations;

namespace SupplyRecallWatch.Api.Models;

public class ItemPatch
{
    [MaxLength(200)]
    public string? Name { get; set; }

    [Range(0, int.MaxValue)]
    public int? Quantity { get; set; }
}