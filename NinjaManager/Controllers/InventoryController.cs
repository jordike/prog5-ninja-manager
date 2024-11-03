using Microsoft.AspNetCore.Mvc;
using NinjaManager.Models;

namespace NinjaManager.Controllers;

public class InventoryController : Controller
{

    private readonly NinjaManagerContext context;

    public InventoryController(NinjaManagerContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
}
