using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaManager.Models
{
    public class NinjaHasEquipment
    {
        [ForeignKey("Ninja")]
        public int NinjaId { get; set; }

        [ForeignKey("Equipment")]
        public int EquipmentId { get; set; }

    }
}
