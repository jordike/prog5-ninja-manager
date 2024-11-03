using Microsoft.AspNetCore.Mvc;
using NinjaManager.BusinessLogic.Services;
using NinjaManager.Data.Models;
using System.Diagnostics;

namespace NinjaManager.Controllers;

/// <summary>  
/// Controller for managing ninja-related operations.  
/// </summary>  
public class NinjaController : Controller
{
    /// <summary>  
    /// Service for managing ninja-related operations.  
    /// </summary>  
    private readonly NinjaService _ninjaService;

    /// <summary>  
    /// Initializes a new instance of the <see cref="NinjaController"/> class.  
    /// </summary>  
    /// <param name="context">The context for managing ninja-related data.</param>  
    public NinjaController(NinjaManagerContext context)
    {
        this._ninjaService = new NinjaService(context);
    }

    /// <summary>  
    /// Displays a list of all ninjas.  
    /// </summary>  
    /// <returns>A view displaying the list of ninjas.</returns>  
    public IActionResult Index()
    {
        var ninjas = _ninjaService.GetAllNinjas();

        return View(ninjas);
    }

    /// <summary>  
    /// Displays the create ninja form.  
    /// </summary>  
    /// <returns>A view displaying the create ninja form.</returns>  
    public IActionResult Create()
    {
        // Temporary ninja to prevent null reference exception.
        return View(new Ninja());
    }

    /// <summary>  
    /// Handles the creation of a new ninja.  
    /// </summary>  
    /// <param name="ninja">The ninja to create.</param>  
    /// <returns>A redirect to the edit view if successful; otherwise, the create view.</returns>  
    [HttpPost]
    public IActionResult Create(Ninja ninja)
    {
        if (!ModelState.IsValid)
        {
            return View(ninja);
        }

        this._ninjaService.AddNewNinja(ninja);

        ViewBag.OwnedEquipment = this._ninjaService.GetOwnedEquipment(ninja);

        return RedirectToAction("Edit", ninja.Id);

    }

    /// <summary>  
    /// Displays the edit ninja form.  
    /// </summary>  
    /// <param name="id">The ID of the ninja to edit.</param>  
    /// <returns>A view displaying the edit ninja form.</returns>  
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

    /// <summary>  
    /// Handles the editing of an existing ninja.  
    /// </summary>  
    /// <param name="ninja">The ninja to edit.</param>  
    /// <returns>A redirect to the edit view if successful; otherwise, the edit view.</returns>  
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

    /// <summary>  
    /// Displays the delete ninja confirmation form.  
    /// </summary>  
    /// <param name="id">The ID of the ninja to delete.</param>  
    /// <returns>A view displaying the delete ninja confirmation form.</returns>  
    public IActionResult Delete(int id)
    {
        var ninja = this._ninjaService.GetNinja(id);

        if (ninja == null)
        {
            return RedirectToAction("Index");
        }

        return View(ninja);
    }

    /// <summary>  
    /// Handles the deletion of an existing ninja.  
    /// </summary>  
    /// <param name="ninja">The ninja to delete.</param>  
    /// <returns>A redirect to the index view.</returns>  
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

    /// <summary>  
    /// Displays the clean ninja inventory confirmation form.  
    /// </summary>  
    /// <param name="id">The ID of the ninja whose inventory is to be cleaned.</param>  
    /// <returns>A view displaying the clean ninja inventory confirmation form.</returns>  
    public IActionResult CleanNinja(int id)
    {
        var ninja = this._ninjaService.GetNinja(id);

        if (ninja == null)
        {
            return RedirectToAction("Index");
        }

        return View(ninja);
    }

    /// <summary>  
    /// Handles the cleaning of a ninja's inventory.  
    /// </summary>  
    /// <param name="ninja">The ninja whose inventory is to be cleaned.</param>  
    /// <returns>A redirect to the edit view.</returns>  
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

    /// <summary>  
    /// Displays the error view.  
    /// </summary>  
    /// <returns>A view displaying the error information.</returns>  
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
