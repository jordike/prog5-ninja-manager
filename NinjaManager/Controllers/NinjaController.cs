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
