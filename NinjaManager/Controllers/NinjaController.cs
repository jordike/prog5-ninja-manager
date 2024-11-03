using Microsoft.AspNetCore.Mvc;
using NinjaManager.BusinessLogic.Services;
using NinjaManager.Data.Models;
using System.Diagnostics;

namespace NinjaManager.Controllers;

public class NinjaController : Controller
{
    private readonly NinjaService _ninjaService;

    public NinjaController(NinjaManagerContext context)
    {
        this._ninjaService = new NinjaService(context);
    }

    public IActionResult Index()
    {
        var ninjas = _ninjaService.GetAllNinjas();

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
            this._ninjaService.AddNewNinja(ninja);

            ViewBag.OwnedEquipment = this._ninjaService.GetOwnedEquipment(ninja);

            return RedirectToAction("Edit", ninja.Id);
        }

        return View(ninja);
    }

    public IActionResult Edit(int id)
    {
        var ninja = this._ninjaService.GetNinja(id);

        if (ninja == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.OwnedEquipment = this._ninjaService.GetOwnedEquipment(ninja);
        ViewBag.TotalValue = this._ninjaService.getTotalValue(id);

        return View(ninja);
    }

    [HttpPost]
    public IActionResult Edit(Ninja ninja)
    {
        ViewBag.OwnedEquipment = this._ninjaService.GetOwnedEquipment(ninja);

        if (!ModelState.IsValid)
        {
            return View(ninja);
        }

        var ninjaToUpdate = this._ninjaService.GetNinja(ninja.Id);

        if (ninjaToUpdate == null)
        {
            return RedirectToAction("Index");
        }

        ninjaToUpdate.Name = ninja.Name;
        ninjaToUpdate.Gold = ninja.Gold;

        this._ninjaService.UpdateNinja(ninjaToUpdate);

        TempData["SuccessMessage"] = "De ninja is bewerkt.";

        return RedirectToAction("Edit", ninja);
    }

    public IActionResult Delete(int id)
    {
        var ninja = this._ninjaService.GetNinja(id);

        if (ninja == null)
        {
            return RedirectToAction("Index");
        }

        return View(ninja);
    }

    [HttpPost]
    public IActionResult Delete(Ninja ninja)
    {
        var ninjaToDelete = this._ninjaService.GetNinja(ninja.Id);

        if (ninjaToDelete == null)
        {
            return RedirectToAction("Index");
        }

        this._ninjaService.DeleteNinja(ninjaToDelete);

        TempData["SuccessMessage"] = "De ninja is verwijderd.";

        return RedirectToAction("Index");
    }

    public IActionResult CleanNinja(int id)
    {
        var ninja = this._ninjaService.GetNinja(id);

        if (ninja == null)
        {
            return RedirectToAction("Index");
        }

        return View(ninja);
    }

    [HttpPost]
    public IActionResult CleanNinja(Ninja ninja)
    {
        if (this._ninjaService.CleanNinjaInventory(ninja))
        {
            TempData["SuccessMessage"] = "De inventory van de ninja is schoongemaakt.";
        }
        else
        {
            TempData["ErrorMessage"] = "Er is iets fout gegaan bij het leegmaken van de inventory.";
        }

        return RedirectToAction("Edit", ninja.Id);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
