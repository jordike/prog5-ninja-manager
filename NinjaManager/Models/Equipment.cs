using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaManager.Models;

public class Equipment
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(1)]
    public string Name { get; set; }

    [Required]
    public int EquipmentTypeId { get; set; }

    [ForeignKey("EquipmentTypeId")]
    public EquipmentType EquipmentType { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Strength { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Agility { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Intelligence { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Value { get; set; }

    public ICollection<NinjaHasEquipment> NinjaHasEquipment { get; set; }
}
