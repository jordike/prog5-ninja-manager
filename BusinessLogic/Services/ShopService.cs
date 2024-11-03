using NinjaManager.Data.Models;

namespace NinjaManager.BusinessLogic.Services;

/// <summary>
/// Service for handling shop-related operations.
/// </summary>
public class ShopService
{
    private readonly NinjaManagerContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShopService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public ShopService(NinjaManagerContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Gets the equipment by its identifier.
    /// </summary>
    /// <param name="id">The equipment identifier.</param>
    /// <returns>The equipment if found; otherwise, null.</returns>
    public Equipment? GetEquipment(int id)
    {
        return this.context.Equipment.Find(id);
    }

    /// <summary>
    /// Gets all equipment.
    /// </summary>
    /// <returns>A list of all equipment.</returns>
    public List<Equipment> GetAllEquipment()
    {
        return this.context.Equipment.ToList();
    }

    /// <summary>
    /// Gets the equipment owned by a specific ninja.
    /// </summary>
    /// <param name="id">The ninja identifier.</param>
    /// <returns>A list of equipment owned by the ninja.</returns>
    public List<NinjaHasEquipment> GetOwnedEquipment(int id)
    {
        return this.context.NinjaHasEquipment
            .Where(nhe => nhe.NinjaId == id)
            .ToList();
    }

    /// <summary>
    /// Gets all equipment of a specific type.
    /// </summary>
    /// <param name="equipmentTypeId">The equipment type identifier.</param>
    /// <returns>A list of equipment of the specified type.</returns>
    public List<Equipment> GetAllEquipmentOfTypeId(int equipmentTypeId)
    {
        return this.context.Equipment
            .Where(equipment => equipment.EquipmentTypeId == equipmentTypeId)
            .ToList();
    }

    /// <summary>
    /// Determines whether the ninja has enough gold.
    /// </summary>
    /// <param name="ninja">The ninja.</param>
    /// <param name="gold">The amount of gold.</param>
    /// <returns><c>true</c> if the ninja has enough gold; otherwise, <c>false</c>.</returns>
    public bool NinjaHasEnoughGold(Ninja ninja, int gold)
    {
        return ninja.Gold >= gold;
    }

    /// <summary>
    /// Determines whether a specific equipment type slot is occupied by the ninja.
    /// </summary>
    /// <param name="ninja">The ninja.</param>
    /// <param name="equipmentTypeId">The equipment type identifier.</param>
    /// <returns><c>true</c> if the equipment type slot is occupied; otherwise, <c>false</c>.</returns>
    public bool IsEquipmentTypeSlotOccupied(Ninja ninja, int equipmentTypeId)
    {
        return this.context.NinjaHasEquipment
            .Any(nhe => nhe.NinjaId == ninja.Id &&
                this.context.Equipment
                    .Where(e => e.Id == nhe.EquipmentId)
                    .Select(e => e.EquipmentTypeId)
                    .FirstOrDefault() == equipmentTypeId);
    }

    /// <summary>
    /// Buys the specified equipment for the ninja.
    /// </summary>
    /// <param name="ninja">The ninja.</param>
    /// <param name="equipment">The equipment.</param>
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

    /// <summary>
    /// Sells the specified equipment owned by the ninja.
    /// </summary>
    /// <param name="ninja">The ninja.</param>
    /// <param name="ninjaHasEquipment">The equipment owned by the ninja.</param>
    public void SellEquipment(Ninja ninja, NinjaHasEquipment ninjaHasEquipment)
    {
        ninja.Gold += ninjaHasEquipment.ValuePaid;

        this.context.Ninjas.Update(ninja);
        this.context.NinjaHasEquipment.Remove(ninjaHasEquipment);

        this.context.SaveChanges();
    }
}
