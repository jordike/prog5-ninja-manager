using Microsoft.EntityFrameworkCore;
using NinjaManager.Data.Models;

namespace NinjaManager.BusinessLogic.Services;

/// <summary>
/// Service class for managing Ninja entities and their related operations.
/// </summary>
public class NinjaService
{
    private NinjaManagerContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="NinjaService"/> class.
    /// </summary>
    /// <param name="context">The database context to be used by the service.</param>
    public NinjaService(NinjaManagerContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Retrieves all ninjas from the database.
    /// </summary>
    /// <returns>A list of all ninjas.</returns>
    public List<Ninja> GetAllNinjas()
    {
        return context.Ninjas.ToList();
    }

    /// <summary>
    /// Adds a new ninja to the database.
    /// </summary>
    /// <param name="ninja">The ninja entity to be added.</param>
    public void AddNewNinja(Ninja ninja)
    {
        this.context.Add(ninja);
        this.context.SaveChanges();
    }

    /// <summary>
    /// Retrieves a ninja by its ID.
    /// </summary>
    /// <param name="id">The ID of the ninja to be retrieved.</param>
    /// <returns>The ninja entity if found; otherwise, null.</returns>
    public Ninja? GetNinja(int id)
    {
        return this.context.Ninjas.Find(id);
    }

    /// <summary>
    /// Retrieves the equipment owned by a specific ninja.
    /// </summary>
    /// <param name="ninja">The ninja whose equipment is to be retrieved.</param>
    /// <returns>A list of equipment owned by the ninja.</returns>
    public List<Equipment> GetOwnedEquipment(Ninja ninja)
    {
        var ninjaEquipment = this.context.NinjaHasEquipment.Where(nhe => nhe.NinjaId == ninja.Id).ToList();
        var equipment = this.context.Equipment.Include(e => e.EquipmentType).ToList();
        var ownedEquipment = equipment.Where(e => ninjaEquipment.Any(nhe => nhe.EquipmentId == e.Id)).ToList();

        return ownedEquipment;
    }

    /// <summary>
    /// Calculates the total value of equipment owned by a specific ninja.
    /// </summary>
    /// <param name="id">The ID of the ninja.</param>
    /// <returns>The total value of the equipment owned by the ninja.</returns>
    public int GetTotalValue(int id)
    {
        return this.context.NinjaHasEquipment.Where(nhe => nhe.NinjaId == id).Sum(nhe => nhe.ValuePaid);
    }

    /// <summary>
    /// Updates the details of an existing ninja.
    /// </summary>
    /// <param name="ninja">The ninja entity with updated details.</param>
    public void UpdateNinja(Ninja ninja)
    {
        this.context.Ninjas.Update(ninja);
        this.context.SaveChanges();
    }

    /// <summary>
    /// Deletes a ninja from the database.
    /// </summary>
    /// <param name="ninja">The ninja entity to be deleted.</param>
    public void DeleteNinja(Ninja ninja)
    {
        // Cleanup inventory
        this.context.NinjaHasEquipment
            .Where(nhe => nhe.NinjaId == ninja.Id).ToList()
            .ForEach(nhe => this.context.NinjaHasEquipment.Remove(nhe));

        // Mark ninja for removal.
        this.context.Ninjas.Remove(ninja);
        this.context.SaveChanges();
    }

    /// <summary>
    /// Cleans the inventory of a specific ninja, returning the value of the equipment to the ninja's gold.
    /// </summary>
    /// <param name="ninja">The ninja whose inventory is to be cleaned.</param>
    /// <returns>True if the inventory was successfully cleaned; otherwise, false.</returns>
    public bool CleanNinjaInventory(Ninja ninja)
    {
        var ninjaEquipment = this.context.NinjaHasEquipment.Where(nhe => nhe.NinjaId == ninja.Id).ToList();
        var ninjaToUpdate = this.GetNinja(ninja.Id);

        if (ninjaToUpdate == null)
        {
            return false;
        }

        foreach (var nhe in ninjaEquipment)
        {
            ninjaToUpdate.Gold += nhe.ValuePaid;

            this.context.NinjaHasEquipment.Remove(nhe);
        }

        this.context.SaveChanges();

        return true;
    }
}
