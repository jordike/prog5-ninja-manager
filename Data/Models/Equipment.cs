using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaManager.Data.Models;

/// <summary>
/// Represents an equipment entity with various attributes and relationships.
/// </summary>
public class Equipment
{
    /// <summary>
    /// Gets or sets the unique identifier for the equipment.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the equipment.
    /// </summary>
    [Required]
    [MinLength(1)]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the equipment type.
    /// </summary>
    [Required]
    public int EquipmentTypeId { get; set; }

    /// <summary>
    /// Gets or sets the equipment type associated with this equipment.
    /// </summary>
    [ForeignKey("EquipmentTypeId")]
    public EquipmentType EquipmentType { get; set; }

    /// <summary>
    /// Gets or sets the strength attribute of the equipment.
    /// </summary>
    [Required]
    [Range(0, int.MaxValue)]
    public int Strength { get; set; }

    /// <summary>
    /// Gets or sets the agility attribute of the equipment.
    /// </summary>
    [Required]
    [Range(0, int.MaxValue)]
    public int Agility { get; set; }

    /// <summary>
    /// Gets or sets the intelligence attribute of the equipment.
    /// </summary>
    [Required]
    [Range(0, int.MaxValue)]
    public int Intelligence { get; set; }

    /// <summary>
    /// Gets or sets the value of the equipment.
    /// </summary>
    [Required]
    [Range(0, int.MaxValue)]
    public int Value { get; set; }

    /// <summary>
    /// Gets or sets the collection of NinjaHasEquipment entities associated with this equipment.
    /// </summary>
    public ICollection<NinjaHasEquipment> NinjaHasEquipment { get; set; }
}
