using CargoWorld.Data;
using CargoWorld.Data.Repositories;
using CargoWorld.Models;
using CargoWorld.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace CargoWorld.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private GroupRepository _groupsRepository;
        private UserManager<ApplicationUser> _userManager;
        private CarRepository _carManager;

        public GroupsController(IRepository<Group> groupsRepository, 
            UserManager<ApplicationUser> userManager,
            IRepository<Car> carManager)
        {
            _groupsRepository = (GroupRepository)groupsRepository;
            _userManager = userManager;
            _carManager = (CarRepository)carManager;
        }

        public IActionResult AGroup(string idOfGroup)
        {
            var group = _groupsRepository.Get(Convert.ToInt32(idOfGroup));
            ViewBag.FreeCars = _groupsRepository.GetCarsWithoutGroup(_userManager.GetUserId(HttpContext.User));
           GroupViewModel gvm = new GroupViewModel
            {
                IdGroup = group.IdGroup,
                IdOwner = group.IdOwner,
                GroupName = group.GroupName,
                Cars = _carManager.CarsInRep(group.IdGroup)
            };
            return View(gvm);
        }


        [HttpGet]
        public IActionResult GroupList()
        {
            return View(_groupsRepository.GetAll(_userManager.GetUserId(HttpContext.User)));
        }


        [HttpGet]
        public IActionResult CreateGroup(int? id)
        {

            if (id == null)
                return View(new GroupViewModel
                {
                    Cars = _groupsRepository.GetCarsWithoutGroup(id.ToString())
                });
            else
            {
                var group = _groupsRepository.Get((int)id);
                var cars = _groupsRepository.GetCarsWithoutGroup(_userManager.GetUserId(HttpContext.User));
                return View(new GroupViewModel
                {
                    IdGroup = group.IdGroup,
                    IdOwner = group.IdOwner,
                    GroupName = group.GroupName,
                    Cars = cars,

                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroupAsync(GroupViewModel gvm)
        {
            var group = new Group
            {
                IdGroup = gvm.IdGroup,
                GroupName = gvm.GroupName,
                IdOwner = await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User))
            };

           

            if (gvm.IdGroup > 0)
                _groupsRepository.Update(group);
            else
                _groupsRepository.Create(group);

            if (await _groupsRepository.SaveChangesAsync())
                return RedirectToAction("Index", "Home");
            else
                return View(group);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            _groupsRepository.Remove(id);
            await _groupsRepository.SaveChangesAsync();
            return RedirectToAction("GroupList", "Groups");
        }

        [HttpGet]
        [Obsolete("Работает, но низя изменять уже занятую запись")]
        public async Task<IActionResult> AddCarToGroupAsync(int idCar, int idGroup)
        {
            var cvm = _carManager.Get(idCar);
            var group = _groupsRepository.Get(idGroup);
            Car carWithGroup = new Car
            {
                IdOwner = cvm.IdOwner,
                IdCar = cvm.IdCar,
                IdDriver = cvm.IdDriver,
                IdGroup = group,
                CarModel = cvm.CarModel,
                CarcassNumber = cvm.CarcassNumber,
                RegistrationNumber = cvm.RegistrationNumber,
                Photo = cvm.Photo,
                Color = cvm.Color,
                CargoType = cvm.CargoType,
                CarType = cvm.CarType,
                CarryingCapacity = cvm.CarryingCapacity,
                CarryingCapacitySqM = cvm.CarryingCapacitySqM,
                HeightCargoCompartment = cvm.HeightCargoCompartment,
                WidthCargoCompartment = cvm.WidthCargoCompartment,
                LengthCargoCompartment = cvm.LengthCargoCompartment,
                CostPerKm = cvm.CostPerKm
            };
            
            _carManager.Update(carWithGroup);
            await _carManager.SaveChangesAsync();
            return RedirectToAction("AGroup", idGroup.ToString());
        }

    }
}
