using Microsoft.AspNetCore.Mvc;
using NinjaManager.Models;

namespace NinjaManager.Controllers;

public class NinjaController : Controller
{
    private readonly NinjaManagerContext context;

    public NinjaController(NinjaManagerContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var ninjaList = this.context.Ninjas.ToList();

        return View(ninjaList);
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
            this.context.Add(
                new Ninja
                {
                    Name = ninja.Name,
                    Gold = ninja.Gold
                }
            );
            this.context.SaveChanges();

            this.RepopulateOwnedEquipment(ninja.Id);

            return RedirectToAction("Edit", ninja.Id);
        }

        return View(ninja);
    }

    public IActionResult Edit(int id)
    {
        var ninja = this.context.Ninjas.Find(id);

        if (ninja == null)
        {
            return RedirectToAction("Index");
        }

        var ninjaEquipment = this.context.NinjaHasEquipment.Where(nhe => nhe.NinjaId == id).ToList();
        var equipment = this.context.Equipment.ToList();
        var ownedEquipment = equipment.Where(e => ninjaEquipment.Any(nhe => nhe.EquipmentId == e.Id)).ToList();

        ViewBag.OwnedEquipment = ownedEquipment;

        return View(ninja);
    }

    [HttpPost]
    public IActionResult Edit(Ninja ninja)
    {
        this.RepopulateOwnedEquipment(ninja.Id);

        if (!ModelState.IsValid)
        {
            return View(ninja);
        }

        var ninjaToUpdate = this.context.Ninjas.Find(ninja.Id);

        if (ninjaToUpdate == null)
        {
            return RedirectToAction("Index");
        }

        ninjaToUpdate.Name = ninja.Name;
        ninjaToUpdate.Gold = ninja.Gold;

        this.context.SaveChanges();

        TempData["SuccessMessage"] = "De ninja is bewerkt.";

        return RedirectToAction("Edit", ninja);
    }

    public IActionResult Delete(int id)
    {
        var ninja = this.context.Ninjas.Find(id);

        if (ninja == null)
        {
            return RedirectToAction("Index");
        }

        return View(ninja);
    }

    [HttpPost]
    public IActionResult Delete(Ninja ninja)
    {
        var ninjaToDelete = this.context.Ninjas.Find(ninja.Id);

        if (ninjaToDelete == null)
        {
            return RedirectToAction("Index");
        }

        this.context.Ninjas.Remove(ninjaToDelete);
        this.context.SaveChanges();

        TempData["SuccessMessage"] = "De ninja is verwijderd.";

        return RedirectToAction("Index");
    }

    public IActionResult CleanNinja(int id)
    {
        var ninja = this.context.Ninjas.Find(id);

        if (ninja == null)
        {
            return RedirectToAction("Index");
        }

        return View(ninja);
    }

    [HttpPost]
    public IActionResult CleanNinja(Ninja ninja)
    {
        var ninjaEquipment = this.context.NinjaHasEquipment.Where(nhe => nhe.NinjaId == ninja.Id).ToList();
        var ninjaToUpdate = this.context.Ninjas.Find(ninja.Id);

        if (ninjaToUpdate != null)
        {
            foreach (var nhe in ninjaEquipment)
            {
                ninjaToUpdate.Gold += nhe.ValuePaid;

                this.context.NinjaHasEquipment.Remove(nhe);
            }

            this.context.SaveChanges();

            TempData["SuccessMessage"] = "De inventory van de ninja is schoongemaakt.";
        }

        this.RepopulateOwnedEquipment(ninja.Id);

        return RedirectToAction("Edit", ninja.Id);
    }

    private void RepopulateOwnedEquipment(int ninjaId)
    {
        var ninjaEquipment = this.context.NinjaHasEquipment.Where(nhe => nhe.NinjaId == ninjaId).ToList();
        var equipment = this.context.Equipment.ToList();
        var ownedEquipment = equipment.Where(e => ninjaEquipment.Any(nhe => nhe.EquipmentId == e.Id)).ToList();

        ViewBag.OwnedEquipment = ownedEquipment;
    }
}
