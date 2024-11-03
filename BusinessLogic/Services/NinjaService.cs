using Microsoft.EntityFrameworkCore;
using NinjaManager.Data.Models;

namespace NinjaManager.BusinessLogic.Services;

public class NinjaService
{
    private NinjaManagerContext context;

    public NinjaService(NinjaManagerContext context)
    {
        this.context = context;
    }

    public List<Ninja> GetNinjas()
    {
        return context.Ninjas.ToList();
    }

    public void AddNewNinja(Ninja ninja)
    {
        this.context.Add(ninja);
        this.context.SaveChanges();
    }

    public Ninja? GetNinja(int id)
    {
        return this.context.Ninjas.Find(id);
    }

    public List<Equipment> GetOwnedEquipment(Ninja ninja)
    {
        var ninjaEquipment = this.context.NinjaHasEquipment.Where(nhe => nhe.NinjaId == ninja.Id).ToList();
        var equipment = this.context.Equipment.Include(e => e.EquipmentType).ToList();
        var ownedEquipment = equipment.Where(e => ninjaEquipment.Any(nhe => nhe.EquipmentId == e.Id)).ToList();

        return ownedEquipment;
    }

    public int getTotalValue(int id)
    {
        var totalValue = this.context.NinjaHasEquipment.Where(nhe => nhe.NinjaId == id).Sum(nhe => nhe.ValuePaid);
        return totalValue;

    }

    public void UpdateNinja(Ninja ninja)
    {
        this.context.Ninjas.Update(ninja);
        this.context.SaveChanges();
    }

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
