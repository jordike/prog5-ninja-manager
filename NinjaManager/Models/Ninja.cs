using System.ComponentModel.DataAnnotations;

namespace NinjaManager.Models;

public class Ninja
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public int Gold { get; set; }
}