using System.ComponentModel.DataAnnotations;

namespace NinjaManager.Data.Models;

/// <summary>
/// Represents a Ninja entity with properties for Id, Name, and Gold.
/// </summary>
public class Ninja
{
    /// <summary>
    /// Gets or sets the unique identifier for the Ninja.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the Ninja.
    /// The name is required and must have a minimum length of 1.
    /// </summary>
    [Required]
    [MinLength(1)]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the amount of gold the Ninja possesses.
    /// The gold amount is required and must be a non-negative integer.
    /// </summary>
    [Required]
    [Range(0, int.MaxValue)]
    public int Gold { get; set; }
}
