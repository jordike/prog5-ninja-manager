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
        var availableEquipment = context.Equipment
            .Where(e => !context.NinjaHasEquipment
            .Any(nhe => nhe.EquipmentId == e.Id && nhe.NinjaId == 1))
            .ToList();

        return View(availableEquipment);
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Edit(int id)
    {
        return View();
    }

    public IActionResult Delete(int id)
    {
        return View();
    }

    public IActionResult Details(int id)
    {
        return View();
    }
}
