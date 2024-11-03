using Microsoft.AspNetCore.Mvc;
using NinjaManager.BusinessLogic.Services;
using NinjaManager.Data.Models;

namespace NinjaManager.Controllers;

public class ShopController : Controller
{
    private readonly ShopService _shopService;
    private readonly NinjaService _ninjaService;
    private readonly EquipmentService _equipmentService;

    public ShopController(NinjaManagerContext context)
    {
        this._shopService = new ShopService(context);
        this._ninjaService = new NinjaService(context);
        this._equipmentService = new EquipmentService(context);
    }

    public IActionResult Index()
    {
        var ninjaList = this._ninjaService.GetAllNinjas();

        return View(ninjaList);
    }

    public IActionResult Details(int id, int? equipmentTypeId = null)
        {
        ViewBag.EquipmentTypes = this._equipmentService.GetAllEquipmentTypes();

        var ninja = this._ninjaService.GetNinja(id);

        if (ninja == null)
        {
            TempData["Error"] = "Ninja not found.";

            return RedirectToAction("Index");
        }

        var ownedEquipment = this._shopService.GetOwnedEquipment(id);

        ViewBag.NinjaId = id;
        ViewBag.OwnedEquipment = ownedEquipment;
        ViewBag.SelectedFilter = equipmentTypeId;

        var equipment = equipmentTypeId != null
            ? this._shopService.GetAllEquipmentOfTypeId((int)equipmentTypeId)
            : this._shopService.GetAllEquipment();

        return View(equipment);
    }

    [HttpPost]
    public IActionResult Buy(int ninjaId, int equipmentId)
    {
        // Check voor enough money
        var equipment = this._shopService.GetEquipment(equipmentId);

        if (equipment == null)
        {
            TempData["Error"] = "Equipment not found.";

            return RedirectToAction("Details", new { id = ninjaId });
        }

        var ninja = this._ninjaService.GetNinja(ninjaId);

        if (ninja == null)
        {
            TempData["Error"] = "Ninja not found.";

            return RedirectToAction("Details", new { id = ninjaId });
        }

        var enoughGold = this._shopService.NinjaHasEnoughGold(ninja, equipment.Value);

        if (!enoughGold)
        {
            // Error message over niet genoeg geld
            TempData["Error"] = "Not enough gold to purchase this equipment.";

            return RedirectToAction("Details", new { id = ninjaId });
        }

        // Check voor 1 per catogorie
        var occupiedSlot = this._shopService.IsEquipmentTypeSlotOccupied(ninja, equipment.EquipmentTypeId);

        if (occupiedSlot)
        {
            // Error message over geen empty slot
            TempData["Error"] = "You already own a piece of equipment from this category.";

            return RedirectToAction("Details", new { id = ninjaId });
        }

        this._shopService.BuyEquipment(ninja, equipment);

        return RedirectToAction("Details", new { id = ninjaId });
    }

    [HttpPost]
    public IActionResult Sell(int ninjaId, int equipmentId)
    {
        var ninjaHasEquipment = this._equipmentService.GetNinjaHasEquipment(ninjaId, equipmentId);

        if (ninjaHasEquipment == null)
        {
            TempData["Error"] = "Equipment not found in inventory.";

            return RedirectToAction("Details", new { id = ninjaId });
        }

        var ninja = this._ninjaService.GetNinja(ninjaId);

        if (ninja == null)
        {
            TempData["Error"] = "Ninja not found.";

            return RedirectToAction("Details", new { id = ninjaId });
        }

        this._shopService.SellEquipment(ninja, ninjaHasEquipment);

        return RedirectToAction("Details", new { id = ninjaId });
    } 
}
