using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NinjaManager.Models;

namespace NinjaManager.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly NinjaManagerContext context;

        public EquipmentController(NinjaManagerContext context)
        { 
        this.context = context;
        }


        public IActionResult Index()
        {

            var equipment = this.context.Equipment.Include(e => e.EquipmentType)
                .ToList();

            return View(equipment);
        }

        public IActionResult Create()
        {
            ViewBag.EquipmentTypes = this.context.EquipmentTypes.ToList();
            var Equipment = new Equipment();
            
            return View(Equipment);
        }

        [HttpPost]
        public IActionResult Create(Equipment equipment)
        {
            this.context.Add(
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
            this.context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id) 
        {
            ViewBag.EquipmentTypes = this.context.EquipmentTypes.ToList();
            var equipment = this.context.Equipment.Find(id);
            
            if (equipment == null) 
            {
                return RedirectToAction("Index");
            }
            
            return View(equipment);
        }

        [HttpPost]
        public IActionResult Edit(Equipment equipment)
        {
            var equipmentToUpdate = this.context.Equipment.Find(equipment.Id);

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

            this.context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var equipment = this.context.Equipment.Find(id);

            if (equipment == null)
            {
                return RedirectToAction("Index");
            }

            var NinjaHasEquipmentCount = this.context.NinjaHasEquipment
                .Count(nhe => nhe.EquipmentId == equipment.Id);

            TempData["Count"] = NinjaHasEquipmentCount;

            return View(equipment);
        }

        [HttpPost]
        public IActionResult Delete(Equipment equipment)
        {
            var EquipmentToDelete = this.context.Equipment.Find(equipment.Id);

            if (EquipmentToDelete == null)
            {
                return RedirectToAction("Index");
            }

            var NinjaHasEquipment = this.context.NinjaHasEquipment
                .Where(nhe => nhe.EquipmentId == equipment.Id)
                .ToList();

            foreach (var NinjaHasEquipmentToDelete in NinjaHasEquipment)
            {
                var NinjaToUpdate = this.context.Ninjas.Find(NinjaHasEquipmentToDelete.NinjaId);
                NinjaToUpdate.Gold += NinjaHasEquipmentToDelete.ValuePaid;
                this.context.NinjaHasEquipment.Remove(NinjaHasEquipmentToDelete);
            }

            this.context.Equipment.Remove(EquipmentToDelete);
            this.context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
