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
        this.context.Add(
            new Ninja
            {
                Name = ninja.Name,
                Gold = ninja.Gold
            }
        );
        this.context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var ninja = this.context.Ninjas.Find(id);

        if (ninja == null)
        {
            return RedirectToAction("Index");
        }

        return View(ninja);
    }

    [HttpPost]
    public IActionResult Edit(Ninja ninja)
    {
        var ninjaToUpdate = this.context.Ninjas.Find(ninja.Id);

        if (ninjaToUpdate == null)
        {
            return RedirectToAction("Index");
        }

        ninjaToUpdate.Name = ninja.Name;
        ninjaToUpdate.Gold = ninja.Gold;

        this.context.SaveChanges();

        return RedirectToAction("Index");
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

    public IActionResult Details(int id)
    {
        return View();
    }
}
