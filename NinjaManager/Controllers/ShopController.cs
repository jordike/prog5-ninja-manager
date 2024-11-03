using Microsoft.AspNetCore.Mvc;
using NinjaManager.Data.Models;

namespace NinjaManager.Controllers;

public class ShopController : Controller
{
    private readonly NinjaManagerContext context;

    public ShopController(NinjaManagerContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var ninjaList = this.context.Ninjas.ToList();

        return View(ninjaList);
    }

    public IActionResult Details(int id, int? equipmentTypeId = null)
    {
        ViewBag.EquipmentTypes = this.context.EquipmentTypes.ToList();

        var ownedEquipment = this.context.NinjaHasEquipment
        .Where(nhe => nhe.NinjaId == id)
        .ToList();

        ViewBag.ownedEquipmentId = ownedEquipment.Select(nhe => nhe.EquipmentId);
        ViewBag.NinjaId = id;
        ViewBag.OwnedEquipment = ownedEquipment;
        ViewBag.SelectedFilter = equipmentTypeId;

        List<Equipment> equipment;

        if (equipmentTypeId != null)
        {
            equipment = this.context.Equipment
                .Where(e => e.EquipmentTypeId == equipmentTypeId.Value)
                .ToList();
        }
        else
        {
            equipment = this.context.Equipment.ToList();
        }
        return View(equipment);
    }

    [HttpPost]
    public IActionResult Buy(int NinjaId, int EquipmentId)
    {
        //check voor enough money
        var equipmentValue = this.context.Equipment
            .Where(e => e.Id == EquipmentId)
            .Select(e => e.Value)
            .FirstOrDefault();

        bool enoughGold = this.context.Ninjas
            .Any(n => n.Id == NinjaId && n.Gold >= equipmentValue);

        if (enoughGold)
        {
            //check voor 1 per catogorie
            var newEquipmentTypeId = this.context.Equipment
                .Where(e => e.Id == EquipmentId)
                .Select(e => e.EquipmentTypeId)
                .FirstOrDefault();

            bool occupiedSlot = this.context.NinjaHasEquipment
                .Any(nhe => nhe.NinjaId == NinjaId &&
                    this.context.Equipment
                        .Where(e => e.Id == nhe.EquipmentId)
                        .Select(e => e.EquipmentTypeId)
                        .FirstOrDefault() == newEquipmentTypeId);

            if (!occupiedSlot)
            {
                //schrijven naar db
                var ninja = this.context.Ninjas.Find(NinjaId);
                if (ninja != null)
                {
                    ninja.Gold -= equipmentValue;
                    this.context.NinjaHasEquipment.Add(new NinjaHasEquipment
                    {
                        NinjaId = NinjaId,
                        EquipmentId = EquipmentId,
                        ValuePaid = equipmentValue
                    });
                }

                this.context.SaveChanges();

                return RedirectToAction("Details", new { id = NinjaId });
            }
            else
            {
                //error message over geen empty slot
                TempData["Error"] = "You already own a piece of equipment from this categorie.";
                return RedirectToAction("Details", new { id = NinjaId });
            }
        }
        else
        {
            //error message over niet genoeg geld
            TempData["Error"] = "Not enough gold to purchase this equipment.";
            return RedirectToAction("Details", new { id = NinjaId });
        }
    }

    [HttpPost]
    public IActionResult Sell(int NinjaId, int EquipmentId)
    {
        var ninjaHasEquipment = this.context.NinjaHasEquipment
        .FirstOrDefault(nhe => nhe.NinjaId == NinjaId && nhe.EquipmentId == EquipmentId);

        if (ninjaHasEquipment != null)
        {
            var valuePaid = ninjaHasEquipment.ValuePaid;
            var ninja = this.context.Ninjas.FirstOrDefault(n => n.Id == NinjaId);

            if (ninja != null)
            {
                ninja.Gold += valuePaid;

                this.context.NinjaHasEquipment.Remove(ninjaHasEquipment);

                this.context.SaveChanges();
            }
            else
            {
                TempData["Error"] = "Ninja not found.";
                return RedirectToAction("Details", new { id = NinjaId });
            }
        }
        else
        {
            TempData["Error"] = "Equipment not found in inventory.";
            return RedirectToAction("Details", new { id = NinjaId });
        }

        return RedirectToAction("Details", new { id = NinjaId });
    } 
}
