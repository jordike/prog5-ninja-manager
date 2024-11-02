using System.ComponentModel.DataAnnotations;

namespace NinjaManager.Models;

public class Equipment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Type { get; set; }

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
