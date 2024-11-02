using System.ComponentModel.DataAnnotations;

namespace NinjaManager.Models
{
    public class EquipmentType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Equipment> Equipment { get; set; }
    }
}
