using Microsoft.AspNetCore.Mvc;
using NinjaManager.BusinessLogic.Services;
using NinjaManager.Data.Models;

namespace NinjaManager.Controllers;

/// <summary>
/// Controller to manage equipment-related operations.
/// </summary>
public class EquipmentController : Controller
{
    /// <summary>
    /// Service to manage equipment-related operations.
    /// </summary>
    private readonly EquipmentService _equipmentService;

    /// <summary>
    /// Initializes a new instance of the <see cref="EquipmentController"/> class.
    /// </summary>
    /// <param name="context">The database context to be used by the controller.</param>
    public EquipmentController(NinjaManagerContext context)
    {
        this._equipmentService = new EquipmentService(context);
    }

    /// <summary>
    /// Displays a list of all equipment.
    /// </summary>
    /// <returns>A view displaying the list of equipment.</returns>
    public IActionResult Index()
    {
        var equipment = this._equipmentService.GetAllEquipment();

        return View(equipment);
    }

    /// <summary>
    /// Displays the form to create new equipment.
    /// </summary>
    /// <returns>A view displaying the form to create new equipment.</returns>
    public IActionResult Create()
    {
        ViewBag.EquipmentTypes = this._equipmentService.GetAllEquipmentTypes();

        // Temporary equipment to prevent null reference exception.
        return View(new Equipment());
    }

    /// <summary>
    /// Creates new equipment and adds it to the database.
    /// </summary>
    /// <param name="equipment">The equipment to be created.</param>
    /// <returns>A redirect to the Index action.</returns>
    [HttpPost]
    public IActionResult Create(Equipment equipment)
    {
        this._equipmentService.AddNewEquipment(
            new Equipment
            {
                Name = equipment.Name,
                EquipmentTypeId = equipment.EquipmentTypeId,
                Strength = equipment.Strength,
                Agility = equipment.Agility,
                Intelligence = equipment.Intelligence,
                Value = equipment.Value
            }
        );

        return RedirectToAction("Index");
    }

    /// <summary>
    /// Displays the form to edit existing equipment.
    /// </summary>
    /// <param name="id">The ID of the equipment to be edited.</param>
    /// <returns>A view displaying the form to edit the equipment.</returns>
    public IActionResult Edit(int id)
    {
        ViewBag.EquipmentTypes = this._equipmentService.GetAllEquipmentTypes();

        var equipment = this._equipmentService.GetEquipment(id);

        if (equipment == null)
        {
            return RedirectToAction("Index");
        }

        return View(equipment);
    }

    /// <summary>
    /// Updates the specified equipment in the database.
    /// </summary>
    /// <param name="equipment">The equipment to be updated.</param>
    /// <returns>A redirect to the Index action.</returns>
    [HttpPost]
    public IActionResult Edit(Equipment equipment)
    {
        var equipmentToUpdate = this._equipmentService.GetEquipment(equipment.Id);

        if (equipmentToUpdate == null)
        {
            return RedirectToAction("Index");
        }

        equipmentToUpdate.Name = equipment.Name;
        equipmentToUpdate.EquipmentTypeId = equipment.EquipmentTypeId;
        equipmentToUpdate.Strength = equipment.Strength;
        equipmentToUpdate.Agility = equipment.Agility;
        equipmentToUpdate.Intelligence = equipment.Intelligence;
        equipmentToUpdate.Value = equipment.Value;

        this._equipmentService.UpdateEquipment(equipmentToUpdate);

        return RedirectToAction("Index");
    }

    /// <summary>
    /// Displays the form to delete existing equipment.
    /// </summary>
    /// <param name="id">The ID of the equipment to be deleted.</param>
    /// <returns>A view displaying the form to delete the equipment.</returns>
    public IActionResult Delete(int id)
    {
        var equipment = this._equipmentService.GetEquipment(id);

        if (equipment == null)
        {
            return RedirectToAction("Index");
        }

        TempData["Count"] = this._equipmentService.GetEquipmentUsageCount(equipment);

        return View(equipment);
    }

    /// <summary>
    /// Deletes the specified equipment from the database.
    /// </summary>
    /// <param name="equipment">The equipment to be deleted.</param>
    /// <returns>A redirect to the Index action.</returns>
    [HttpPost]
    public IActionResult Delete(Equipment equipment)
    {
        var equipmentToDelete = this._equipmentService.GetEquipment(equipment.Id);

        if (equipmentToDelete == null)
        {
            return RedirectToAction("Index");
        }

        this._equipmentService.RemoveEquipment(equipmentToDelete);

        return RedirectToAction("Index");
    }
}
