using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NinjaManager.Models;

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

    //public IActionResult Create()
    //{
    //    return View();
    //}

    //public IActionResult Edit(int id)
    //{
    //    return View();
    //}

    //public IActionResult Delete(int id)
    //{
    //    return View();
    //}

    public IActionResult Details(int id)
    {
        var availableEquipment = context.Equipment
            .Where(e => !context.NinjaHasEquipment.Any(nhe => nhe.EquipmentId == e.Id && nhe.NinjaId == id))
            .ToList();

        ViewBag.NinjaId = id;

        return View(availableEquipment);
    }

    [HttpPost]
    public IActionResult Buy(int NinjaId, int EquipmentId)
    {
        //check voor enough money
        var equipmentValue = context.Equipment
            .Where(e => e.Id == EquipmentId)
            .Select(e => e.Value)
            .FirstOrDefault();

        bool enoughGold = context.Ninjas
            .Any(n => n.Id == NinjaId && n.Gold >= equipmentValue);

        if (enoughGold) 
        {
            //check voor 1 per catogorie
            var newEquipmentTypeId = context.Equipment
                .Where(e => e.Id == EquipmentId)
                .Select(e => e.EquipmentTypeId)
                .FirstOrDefault();

            bool occupiedSlot = context.NinjaHasEquipment
                .Any(nhe => nhe.NinjaId == NinjaId &&
                    context.Equipment
                        .Where(e => e.Id == nhe.EquipmentId)
                        .Select(e => e.EquipmentTypeId)
                        .FirstOrDefault() == newEquipmentTypeId);

            if (!occupiedSlot)
            {
                //schrijven naar db
                var ninja = context.Ninjas.Find(NinjaId);
                if (ninja != null)
                {
                    ninja.Gold -= equipmentValue;
                    context.NinjaHasEquipment.Add(new NinjaHasEquipment
                    {
                        NinjaId = NinjaId,
                        EquipmentId = EquipmentId,
                        ValuePaid = equipmentValue
                    });
                }

                context.SaveChanges();

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
}
