using Microsoft.EntityFrameworkCore;
using NinjaManager.Data.Models;

namespace NinjaManager.BusinessLogic.Services;

public class EquipmentService
{
    private readonly NinjaManagerContext context;

    public EquipmentService(NinjaManagerContext context)
    {
        this.context = context;
    }

    public List<Equipment> GetAllEquipment()
    {
        return this.context.Equipment
            .Include(equipment => equipment.EquipmentType)
            .ToList();
    }

    public List<EquipmentType> GetAllEquipmentTypes()
    {
        return this.context.EquipmentTypes.ToList();
    }

    public void AddNewEquipment(Equipment equipment)
    {
        this.context.Add(equipment);
        this.context.SaveChanges();
    }

    public Equipment? GetEquipment(int id)
    {
        return this.context.Equipment.Find(id);
    }

    public void UpdateEquipment(Equipment equipment)
    {
        this.context.Update(equipment);
        this.context.SaveChanges();
    }

    public int GetEquipmentUsageCount(Equipment equipment)
    {
        return this.context.NinjaHasEquipment
            .Count(nhe => nhe.EquipmentId == equipment.Id);
    }

    public void RemoveEquipment(Equipment equipment)
    {
        var ninjaHasEquipment = this.GetNinjaHasEquipments(equipment);

        foreach (var ninjaHasEquipmentToDelete in ninjaHasEquipment)
        {
            var ninjaToUpdate = this.context.Ninjas.Find(ninjaHasEquipmentToDelete.NinjaId);

            if (ninjaToUpdate != null)
            {
                ninjaToUpdate.Gold += ninjaHasEquipmentToDelete.ValuePaid;
            }

            this.context.NinjaHasEquipment.Remove(ninjaHasEquipmentToDelete);
        }

        this.context.Equipment.Remove(equipment);
        this.context.SaveChanges();
    }

    private List<NinjaHasEquipment> GetNinjaHasEquipments(Equipment equipment)
    {
        return this.context.NinjaHasEquipment
            .Where(nhe => nhe.EquipmentId == equipment.Id)
            .ToList();
    }

    public NinjaHasEquipment? GetNinjaHasEquipment(int ninjaId, int equipmentId)
    {
        return this.context.NinjaHasEquipment
            .FirstOrDefault(nhe => nhe.NinjaId == ninjaId && nhe.EquipmentId == equipmentId);
    }
}
