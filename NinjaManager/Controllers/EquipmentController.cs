using Microsoft.AspNetCore.Mvc;
using NinjaManager.BusinessLogic.Services;
using NinjaManager.Data.Models;

namespace NinjaManager.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly EquipmentService equipmentService;

        public EquipmentController(NinjaManagerContext context)
        { 
            this.equipmentService = new EquipmentService(context);
        }

        public IActionResult Index()
        {
            var equipment = this.equipmentService.GetAllEquipment();

            return View(equipment);
        }

        public IActionResult Create()
        {
            ViewBag.EquipmentTypes = this.equipmentService.GetAllEquipmentTypes();

            // Temporary equipment to prevent null reference exception.
            return View(new Equipment());
        }

        [HttpPost]
        public IActionResult Create(Equipment equipment)
        {
            this.equipmentService.AddNewEquipment(
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

        public IActionResult Edit(int id) 
        {
            ViewBag.EquipmentTypes = this.equipmentService.GetAllEquipmentTypes();

            var equipment = this.equipmentService.GetEquipment(id);
            
            if (equipment == null) 
            {
                return RedirectToAction("Index");
            }
            
            return View(equipment);
        }

        [HttpPost]
        public IActionResult Edit(Equipment equipment)
        {
            var equipmentToUpdate = this.equipmentService.GetEquipment(equipment.Id);

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

            this.equipmentService.UpdateEquipment(equipmentToUpdate);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var equipment = this.equipmentService.GetEquipment(id);

            if (equipment == null)
            {
                return RedirectToAction("Index");
            }

            TempData["Count"] = this.equipmentService.GetEquipmentUsageCount(equipment);

            return View(equipment);
        }

        [HttpPost]
        public IActionResult Delete(Equipment equipment)
        {
            var equipmentToDelete = this.equipmentService.GetEquipment(equipment.Id);

            if (equipmentToDelete == null)
            {
                return RedirectToAction("Index");
            }

            this.equipmentService.RemoveEquipment(equipmentToDelete);

            return RedirectToAction("Index");
        }
    }
}
