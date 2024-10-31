using System.ComponentModel.DataAnnotations;

namespace NinjaManager.Models;

public class Equipment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public int Value { get; set; }
}
