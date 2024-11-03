using Microsoft.AspNetCore.Mvc;
using NinjaManager.BusinessLogic.Services;
using NinjaManager.Data.Models;

namespace NinjaManager.Controllers;

public class NinjaController : Controller
{
    private readonly NinjaManagerContext context;
    private readonly NinjaService ninjaService;

    public NinjaController(NinjaManagerContext context)
    {
        this.context = context;
        this.ninjaService = new NinjaService(context);
    }

    public IActionResult Index()
    {
        var ninjas = ninjaService.GetNinjas();

        return View(ninjas);
    }

    public IActionResult Create()
    {
        // Temporary ninja to prevent null reference exception.
        var ninja = new Ninja();

        return View(ninja);
    }

    [HttpPost]
    public IActionResult Create(Ninja ninja)
    {
        if (ModelState.IsValid)
        {
            this.ninjaService.AddNewNinja(ninja);

            ViewBag.OwnedEquipment = this.ninjaService.GetOwnedEquipment(ninja);

            return RedirectToAction("Edit", ninja.Id);
        }

        return View(ninja);
    }

    public IActionResult Edit(int id)
    {
        var ninja = this.ninjaService.GetNinja(id);

        if (ninja == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.OwnedEquipment = this.ninjaService.GetOwnedEquipment(ninja);
        ViewBag.TotalValue = this.ninjaService.getTotalValue(id);

        return View(ninja);
    }

    [HttpPost]
    public IActionResult Edit(Ninja ninja)
    {
        ViewBag.OwnedEquipment = this.ninjaService.GetOwnedEquipment(ninja);

        if (!ModelState.IsValid)
        {
            return View(ninja);
        }

        var ninjaToUpdate = this.ninjaService.GetNinja(ninja.Id);

        if (ninjaToUpdate == null)
        {
            return RedirectToAction("Index");
        }

        ninjaToUpdate.Name = ninja.Name;
        ninjaToUpdate.Gold = ninja.Gold;

        this.ninjaService.UpdateNinja(ninjaToUpdate);

        TempData["SuccessMessage"] = "De ninja is bewerkt.";

        return RedirectToAction("Edit", ninja);
    }

    public IActionResult Delete(int id)
    {
        var ninja = this.ninjaService.GetNinja(id);

        if (ninja == null)
        {
            return RedirectToAction("Index");
        }

        return View(ninja);
    }

    [HttpPost]
    public IActionResult Delete(Ninja ninja)
    {
        var ninjaToDelete = this.ninjaService.GetNinja(ninja.Id);

        if (ninjaToDelete == null)
        {
            return RedirectToAction("Index");
        }

        this.ninjaService.DeleteNinja(ninjaToDelete);

        TempData["SuccessMessage"] = "De ninja is verwijderd.";

        return RedirectToAction("Index");
    }

    public IActionResult CleanNinja(int id)
    {
        var ninja = this.ninjaService.GetNinja(id);

        if (ninja == null)
        {
            return RedirectToAction("Index");
        }

        return View(ninja);
    }

    [HttpPost]
    public IActionResult CleanNinja(Ninja ninja)
    {
        if (this.ninjaService.CleanNinjaInventory(ninja))
        {
            TempData["SuccessMessage"] = "De inventory van de ninja is schoongemaakt.";
        }
        else
        {
            TempData["ErrorMessage"] = "Er is iets fout gegaan bij het leegmaken van de inventory.";
        }

        return RedirectToAction("Edit", ninja.Id);
    }
}
