using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaManager.Models;

public class Equipment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int EquipmentTypeId { get; set; }

    [ForeignKey("EquipmentTypeId")]
    public EquipmentType EquipmentType { get; set; }

    [Required]
    public int Strength { get; set; }

    [Required]
    public int Agility { get; set; }

    [Required]
    public int Intelligence { get; set; }

    [Required]
    public int Value { get; set; }

    public ICollection<NinjaHasEquipment> NinjaHasEquipment { get; set; }
}
