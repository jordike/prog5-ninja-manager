using Microsoft.EntityFrameworkCore;
using NinjaManager.Data.Models;

namespace NinjaManager.BusinessLogic.Services;

/// <summary>
/// Service class for managing equipment-related operations.
/// </summary>
public class EquipmentService
{
    private readonly NinjaManagerContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="EquipmentService"/> class.
    /// </summary>
    /// <param name="context">The database context to be used.</param>
    public EquipmentService(NinjaManagerContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Retrieves all equipment, including their types.
    /// </summary>
    /// <returns>A list of all equipment.</returns>
    public List<Equipment> GetAllEquipment()
    {
        return this.context.Equipment
            .Include(equipment => equipment.EquipmentType)
            .ToList();
    }

    /// <summary>
    /// Retrieves all equipment types.
    /// </summary>
    /// <returns>A list of all equipment types.</returns>
    public List<EquipmentType> GetAllEquipmentTypes()
    {
        return this.context.EquipmentTypes.ToList();
    }

    /// <summary>
    /// Adds new equipment to the database.
    /// </summary>
    /// <param name="equipment">The equipment to be added.</param>
    public void AddNewEquipment(Equipment equipment)
    {
        this.context.Add(equipment);
        this.context.SaveChanges();
    }

    /// <summary>
    /// Retrieves equipment by its ID.
    /// </summary>
    /// <param name="id">The ID of the equipment.</param>
    /// <returns>The equipment if found; otherwise, null.</returns>
    public Equipment? GetEquipment(int id)
    {
        return this.context.Equipment.Find(id);
    }

    /// <summary>
    /// Updates the specified equipment in the database.
    /// </summary>
    /// <param name="equipment">The equipment to be updated.</param>
    public void UpdateEquipment(Equipment equipment)
    {
        this.context.Update(equipment);
        this.context.SaveChanges();
    }

    /// <summary>
    /// Gets the usage count of the specified equipment.
    /// </summary>
    /// <param name="equipment">The equipment to check usage for.</param>
    /// <returns>The number of times the equipment is used.</returns>
    public int GetEquipmentUsageCount(Equipment equipment)
    {
        return this.context.NinjaHasEquipment
            .Count(nhe => nhe.EquipmentId == equipment.Id);
    }

    /// <summary>
    /// Removes the specified equipment from the database.
    /// </summary>
    /// <param name="equipment">The equipment to be removed.</param>
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

    /// <summary>
    /// Retrieves all NinjaHasEquipment entries for the specified equipment.
    /// </summary>
    /// <param name="equipment">The equipment to get entries for.</param>
    /// <returns>A list of NinjaHasEquipment entries.</returns>
    private List<NinjaHasEquipment> GetNinjaHasEquipments(Equipment equipment)
    {
        return this.context.NinjaHasEquipment
            .Where(nhe => nhe.EquipmentId == equipment.Id)
            .ToList();
    }

    /// <summary>
    /// Retrieves a specific NinjaHasEquipment entry by ninja ID and equipment ID.
    /// </summary>
    /// <param name="ninjaId">The ID of the ninja.</param>
    /// <param name="equipmentId">The ID of the equipment.</param>
    /// <returns>The NinjaHasEquipment entry if found; otherwise, null.</returns>
    public NinjaHasEquipment? GetNinjaHasEquipment(int ninjaId, int equipmentId)
    {
        return this.context.NinjaHasEquipment
            .FirstOrDefault(nhe => nhe.NinjaId == ninjaId && nhe.EquipmentId == equipmentId);
    }
}
