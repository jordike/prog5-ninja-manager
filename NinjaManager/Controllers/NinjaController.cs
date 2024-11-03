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
        if (ModelState.IsValid)
        {
            var ninjaToUpdate = this.context.Ninjas.Find(ninja.Id);

            if (ninjaToUpdate == null)
            {
                return RedirectToAction("Index");
            }

            ninjaToUpdate.Name = ninja.Name;
            ninjaToUpdate.Gold = ninja.Gold;

            this.context.SaveChanges();

            return RedirectToAction("Edit", ninja);
        }

        // Re-populate ViewBag.OwnedEquipment
        var ninjaEquipment = this.context.NinjaHasEquipment.Where(nhe => nhe.NinjaId == ninja.Id).ToList();
        var equipment = this.context.Equipment.ToList();
        var ownedEquipment = equipment.Where(e => ninjaEquipment.Any(nhe => nhe.EquipmentId == e.Id)).ToList();

        ViewBag.OwnedEquipment = ownedEquipment;

        return View(ninja);
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

        if (ninjaToUpdate == null || ninjaEquipment.Count == 0)
        {
            return RedirectToAction("Index");
        }

        foreach (var nhe in ninjaEquipment)
        {
            ninjaToUpdate.Gold += nhe.ValuePaid;

            this.context.NinjaHasEquipment.Remove(nhe);
        }

        this.context.SaveChanges();

        return RedirectToAction("Index");
    }
}
