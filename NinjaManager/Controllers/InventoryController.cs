using Microsoft.AspNetCore.Mvc;

namespace NinjaManager.Controllers;

public class InventoryController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
