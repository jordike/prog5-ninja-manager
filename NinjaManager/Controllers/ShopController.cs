using Microsoft.AspNetCore.Mvc;
using NinjaManager.BusinessLogic.Services;
using NinjaManager.Data.Models;

namespace NinjaManager.Controllers;

/// <summary>
/// Controller for handling shop-related actions.
/// </summary>
public class ShopController : Controller
{
    private readonly ShopService _shopService;
    private readonly NinjaService _ninjaService;
    private readonly EquipmentService _equipmentService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShopController"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public ShopController(NinjaManagerContext context)
    {
        this._shopService = new ShopService(context);
        this._ninjaService = new NinjaService(context);
        this._equipmentService = new EquipmentService(context);
    }

    /// <summary>
    /// Displays the list of all ninjas.
    /// </summary>
    /// <returns>A view with the list of ninjas.</returns>
    public IActionResult Index()
    {
        var ninjaList = this._ninjaService.GetAllNinjas();

        return View(ninjaList);
    }

    /// <summary>
    /// Displays the details of a specific ninja, including their equipment.
    /// </summary>
    /// <param name="id">The ID of the ninja.</param>
    /// <param name="equipmentTypeId">The optional ID of the equipment type to filter by.</param>
    /// <returns>A view with the ninja details and equipment.</returns>
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

    /// <summary>
    /// Handles the purchase of equipment for a ninja.
    /// </summary>
    /// <param name="ninjaId">The ID of the ninja.</param>
    /// <param name="equipmentId">The ID of the equipment to purchase.</param>
    /// <returns>A redirect to the ninja details view.</returns>
    [HttpPost]
    public IActionResult Buy(int ninjaId, int equipmentId)
    {
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
            TempData["Error"] = "Not enough gold to purchase this equipment.";

            return RedirectToAction("Details", new { id = ninjaId });
        }

        var occupiedSlot = this._shopService.IsEquipmentTypeSlotOccupied(ninja, equipment.EquipmentTypeId);

        if (occupiedSlot)
        {
            TempData["Error"] = "You already own a piece of equipment from this category.";

            return RedirectToAction("Details", new { id = ninjaId });
        }

        this._shopService.BuyEquipment(ninja, equipment);

        return RedirectToAction("Details", new { id = ninjaId });
    }

    /// <summary>
    /// Handles the sale of equipment from a ninja.
    /// </summary>
    /// <param name="ninjaId">The ID of the ninja.</param>
    /// <param name="equipmentId">The ID of the equipment to sell.</param>
    /// <returns>A redirect to the ninja details view.</returns>
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
