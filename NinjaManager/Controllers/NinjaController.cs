using Microsoft.AspNetCore.Mvc;
using NinjaManager.Models;

namespace NinjaManager.Controllers;

public class NinjaController : Controller
{
    public IActionResult Index()
    {
        // TODO: Test data, change to actual database content.
        List<Ninja> ninja =
        [
            new Ninja
            {
                Id = 1,
                Name = "Test",
                Gold = 100
            },
            new Ninja
            {
                Id = 2,
                Name = "Test2",
                Gold = 200
            },
            new Ninja
            {
                Id = 3,
                Name = "Test3",
                Gold = 300
            },
            new Ninja
            {
                Id = 4,
                Name = "Test4",
                Gold = 400
            },
            new Ninja
            {
                Id = 5,
                Name = "Test5",
                Gold = 500
            }
        ];

        return View(ninja);
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
