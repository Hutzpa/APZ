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
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CargoWorld.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private GroupRepository _groupsRepository;
        private UserManager<ApplicationUser> _userManager;
        private CarRepository _carManager;
        private CargoRepository _cargoManager;
        private UserRepository _userRepository;

        public GroupsController(IRepository<Group> groupsRepository,
            UserManager<ApplicationUser> userManager,
            IRepository<Car> carManager,
            IRepository<Cargo> cargoManager,
            IRepository<ApplicationUser> userRepository)
        {
            _groupsRepository = (GroupRepository)groupsRepository;
            _userManager = userManager;
            _carManager = (CarRepository)carManager;
            _cargoManager = (CargoRepository)cargoManager;
            _userRepository = (UserRepository)userRepository;
        }

        public IActionResult AGroup(string idOfGroup)
        {
            var group = _groupsRepository.Get(Convert.ToInt32(idOfGroup));

            var freeCars = _groupsRepository.GetCarsWithoutGroup(_userManager.GetUserId(HttpContext.User));
            ViewBag.FreeCars = freeCars;
            ViewBag.FreeCarsCount = freeCars.Count();
            GroupViewModel gvm = new GroupViewModel
            {
                IdGroup = (int)group.IdGroup,
                IdOwner = group.IdOwner,
                GroupName = group.GroupName,
                Cars = _carManager.CarsInRep((int)group.IdGroup)
            };
            return View(gvm);
        }


        [HttpGet]
        public IActionResult GroupList(int pageNumber)
        {
            if (pageNumber < 1)
                return RedirectToAction("GroupList", new { pageNumber = 1 });

            var list = _groupsRepository.GetAll(_userManager.GetUserId(HttpContext.User), pageNumber);
            return View(list);
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
                    IdGroup = (int)group.IdGroup,
                    IdOwner = group.IdOwner,
                    GroupName = group.GroupName,
                    Cars = cars,

                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroupAsync(GroupViewModel gvm)
        {
            if (!ModelState.IsValid)
            {
                return View(gvm);
            }
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
            return RedirectToAction("GroupList");
        }

        [HttpGet]
        [Obsolete("Работает, но низя изменять уже занятую запись")]
        public async Task<IActionResult> AddCarToGroupAsync(int idCar, int idGroup)
        {
            var cvm = _carManager.Get(idCar);
            var group = _groupsRepository.Get(idGroup);
            cvm.IdGroup = group;
           
            _carManager.Update(cvm);
            await _carManager.SaveChangesAsync();

            return RedirectToAction("AGroup", new { idOfGroup = idGroup });
        }

        public async Task<IActionResult> DeleteCarFromGroupAsync(int idCar, int idGroup)
        {
            var cvm = _carManager.GetTracking(idCar);
            cvm.IdGroup = null;
            

            _carManager.Update(cvm);
            await _carManager.SaveChangesAsync();
            return RedirectToAction("AGroup", new { idOfGroup = idGroup });
        }


        public IActionResult GetOptimalCargoForGroup(int idGroup)
        {
            var cvm = new OptimalCargoViewModel
            {
                Cargos = _cargoManager.SearchOptimalCargoForGroup(idGroup),
                OurGroup = _groupsRepository.Get(idGroup)
            };
            return View(cvm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOptimalGroupAsync(OptimalGroupViewModel ogvm, int[] items)
        {

            List<Car> carsThisGroup = new List<Car>();

            foreach(int i in items)
            {
                carsThisGroup.Add(_carManager.Get(i));
            }
           

            Group group = new Group()
            {
                IdGroup = 0,
                IdOwner = _userRepository.Get(_userManager.GetUserId(HttpContext.User)),
                GroupName = "GroupFor" + _cargoManager.Get(ogvm.Cargo.Id_Cargo).CargoName,
            };

            _groupsRepository.Create(group);
            
           // await _groupsRepository.SaveChangesAsync();

            foreach(Car car in carsThisGroup)
            {
                car.IdGroup = group;
               
            }

            _groupsRepository.UpdateMany(carsThisGroup); // ошбика отслеживания Пользователя?!
            await _groupsRepository.SaveChangesAsync();

            return RedirectToAction("Index","Home");
        }
    } 
}
