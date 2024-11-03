using Microsoft.AspNetCore.Mvc;
using NinjaManager.BusinessLogic.Services;
using NinjaManager.Data.Models;

namespace NinjaManager.Controllers;

public class ShopController : Controller
{
    private readonly NinjaManagerContext context;
    private readonly ShopService shopService;
    private readonly NinjaService ninjaService;
    private readonly EquipmentService equipmentService;

    public ShopController(NinjaManagerContext context)
    {
        this.context = context;
        this.shopService = new ShopService(context);
        this.ninjaService = new NinjaService(context);
        this.equipmentService = new EquipmentService(context);
    }

    public IActionResult Index()
    {
        var ninjaList = this.ninjaService.GetAllNinjas();

        return View(ninjaList);
    }

    public IActionResult Details(int id, int? equipmentTypeId = null)
    {
        ViewBag.EquipmentTypes = this.context.EquipmentTypes.ToList();

        var ninja = this.ninjaService.GetNinja(id);

        if (ninja == null)
        {
            TempData["Error"] = "Ninja not found.";

            return RedirectToAction("Index");
        }

        var ownedEquipment = this.ninjaService.GetOwnedEquipment(ninja);

        ViewBag.NinjaId = id;
        ViewBag.OwnedEquipment = ownedEquipment;
        ViewBag.SelectedFilter = equipmentTypeId;

        var equipment = equipmentTypeId != null
            ? this.shopService.GetAllEquipmentOfTypeId((int)equipmentTypeId)
            : this.shopService.GetAllEquipment();

        return View(equipment);
    }

    [HttpPost]
    public IActionResult Buy(int ninjaId, int equipmentId)
    {
        // Check voor enough money
        var equipment = this.shopService.GetEquipment(equipmentId);

        if (equipment == null)
        {
            TempData["Error"] = "Equipment not found.";

            return RedirectToAction("Details", new { id = ninjaId });
        }

        var ninja = this.ninjaService.GetNinja(ninjaId);

        if (ninja == null)
        {
            TempData["Error"] = "Ninja not found.";

            return RedirectToAction("Details", new { id = ninjaId });
        }

        var enoughGold = this.shopService.NinjaHasEnoughGold(ninja, equipment.Value);

        if (!enoughGold)
        {
            // Error message over niet genoeg geld
            TempData["Error"] = "Not enough gold to purchase this equipment.";

            return RedirectToAction("Details", new { id = ninjaId });
        }

        // Check voor 1 per catogorie
        var occupiedSlot = this.shopService.IsEquipmentTypeSlotOccupied(ninja, equipment.EquipmentTypeId);

        if (occupiedSlot)
        {
            // Error message over geen empty slot
            TempData["Error"] = "You already own a piece of equipment from this categorie.";

            return RedirectToAction("Details", new { id = ninjaId });
        }

        this.shopService.BuyEquipment(ninja, equipment);

        return RedirectToAction("Details", new { id = ninjaId });
    }

    [HttpPost]
    public IActionResult Sell(int ninjaId, int equipmentId)
    {
        var ninjaHasEquipment = this.equipmentService.GetNinjaHasEquipment(ninjaId, equipmentId);

        if (ninjaHasEquipment == null)
        {
            TempData["Error"] = "Equipment not found in inventory.";

            return RedirectToAction("Details", new { id = ninjaId });
        }

        var ninja = this.context.Ninjas.FirstOrDefault(n => n.Id == ninjaId);

        if (ninja == null)
        {
            TempData["Error"] = "Ninja not found.";

            return RedirectToAction("Details", new { id = ninjaId });
        }

        this.shopService.SellEquipment(ninja, ninjaHasEquipment);

        return RedirectToAction("Details", new { id = ninjaId });
    } 
}
