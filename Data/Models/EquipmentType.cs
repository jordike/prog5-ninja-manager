using System.ComponentModel.DataAnnotations;

namespace NinjaManager.Data.Models;

/// <summary>
/// Represents a type of equipment in the Ninja Manager application.
/// </summary>
public class EquipmentType
{
    /// <summary>
    /// Gets or sets the unique identifier for the equipment type.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the equipment type.
    /// </summary>
    [Required]
    [MinLength(1)]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the collection of equipment associated with this equipment type.
    /// </summary>
    public ICollection<Equipment> Equipment { get; set; }
}
