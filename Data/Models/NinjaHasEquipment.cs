using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaManager.Data.Models;

/// <summary>
/// Represents the relationship between a Ninja and their Equipment.
/// </summary>
public class NinjaHasEquipment
{
    /// <summary>
    /// Gets or sets the ID of the Ninja.
    /// </summary>
    [ForeignKey("Ninja")]
    public int NinjaId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the Equipment.
    /// </summary>
    [ForeignKey("Equipment")]
    public int EquipmentId { get; set; }

    /// <summary>
    /// Gets or sets the value paid for the equipment.
    /// </summary>
    [Required]
    [Range(0, int.MaxValue)]
    public int ValuePaid { get; set; }
}
