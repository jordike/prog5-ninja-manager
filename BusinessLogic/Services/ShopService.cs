using NinjaManager.Data.Models;

namespace NinjaManager.BusinessLogic.Services;

public class ShopService
{
    private readonly NinjaManagerContext context;

    public ShopService(NinjaManagerContext context)
    {
        this.context = context;
    }

    public Equipment? GetEquipment(int id)
    {
        return this.context.Equipment.Find(id);
    }

    public List<Equipment> GetAllEquipment()
    {
        return this.context.Equipment.ToList();
    }

    public List<NinjaHasEquipment> GetOwnedEquipment(int id) 
    {
        return this.context.NinjaHasEquipment
            .Where(nhe => nhe.NinjaId == id)
            .ToList();
    }

    public List<Equipment> GetAllEquipmentOfTypeId(int equipmentTypeId)
    {
        return this.context.Equipment
            .Where(equipment => equipment.EquipmentTypeId == equipmentTypeId)
            .ToList();
    }

    public bool NinjaHasEnoughGold(Ninja ninja, int gold)
    {
        return ninja.Gold >= gold;
    }

    public bool IsEquipmentTypeSlotOccupied(Ninja ninja, int equipmentTypeId)
    {
        return this.context.NinjaHasEquipment
            .Any(nhe => nhe.NinjaId == ninja.Id &&
                this.context.Equipment
                    .Where(e => e.Id == nhe.EquipmentId)
                    .Select(e => e.EquipmentTypeId)
                    .FirstOrDefault() == equipmentTypeId);
    }

    public void BuyEquipment(Ninja ninja, Equipment equipment)
    {
        ninja.Gold -= equipment.Value;
        this.context.Ninjas.Update(ninja);

        this.context.NinjaHasEquipment.Add(new NinjaHasEquipment
        {
            NinjaId = ninja.Id,
            EquipmentId = equipment.Id,
            ValuePaid = equipment.Value
        });

        this.context.SaveChanges();
    }

    public void SellEquipment(Ninja ninja, NinjaHasEquipment ninjaHasEquipment)
    {
        ninja.Gold += ninjaHasEquipment.ValuePaid;

        this.context.Ninjas.Update(ninja);
        this.context.NinjaHasEquipment.Remove(ninjaHasEquipment);

        this.context.SaveChanges();
    }
}
