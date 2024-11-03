using System.ComponentModel.DataAnnotations;

namespace NinjaManager.Models;

public class Ninja
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(1)]
    public string Name { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Gold { get; set; }

    public List<NinjaHasEquipment> NinjaHasEquipment { get; set; }
}