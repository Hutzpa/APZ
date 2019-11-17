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
    public class CarController : Controller
    {
        private IRepository<Car> _carRepository;
        private UserManager<ApplicationUser> _userManager;

        public CarController(IRepository<Car> carRepository, UserManager<ApplicationUser> userManager)
        {
            _carRepository = carRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> CreateCarAsync(int? id)
        {
            if (id == null)
                return View(new CarViewModel());
            else
            {
                var car = _carRepository.Get((int)id);
                return View(new CarViewModel
                {
                    IdOwner = await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)),
                    IdCar = car.IdCar,
                    IdDriver = car.IdDriver,
                    IdGroup = null,
                    CarModel = car.CarModel,
                    CarcassNumber = car.CarcassNumber,
                    RegistrationNumber = car.RegistrationNumber,
                    Photo = car.Photo,
                    Color = car.Color,
                    CargoType = car.CargoType,
                    CarType = car.CarType,
                    CarryingCapacity = car.CarryingCapacity,
                    CarryingCapacitySqM = car.CarryingCapacitySqM,
                    HeightCargoCompartment = car.HeightCargoCompartment,
                    WidthCargoCompartment = car.WidthCargoCompartment,
                    LengthCargoCompartment = car.LengthCargoCompartment,
                    CostPerKm = car.CostPerKm
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(CarViewModel cvm)
        {
            var car = new Car
            {
                IdOwner = await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)),
                IdCar = cvm.IdCar,
                IdDriver = cvm.IdDriver,
                IdGroup = cvm.IdGroup,
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


            if (cvm.IdCar > 0)
                _carRepository.Update(car);
            else
                _carRepository.Create(car);

            if (await _carRepository.SaveChangesAsync())
                return RedirectToAction("Index", "Home");
            else
                return View(car);
        }


        public IActionResult ACar(int id)
        {
            var cvm = _carRepository.Get(id);
            CarViewModel car = new CarViewModel
            {
                IdOwner = cvm.IdOwner,
                IdCar = cvm.IdCar,
                IdDriver = cvm.IdDriver,
                IdGroup = cvm.IdGroup,
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
            return View(car);
        }


        [HttpGet]
        public IActionResult ImDriving(int id)
        {
            //var res = _carRepository.IAmDriving(id);
            return View(/*res*/);
        }

        [HttpGet]
        public IActionResult MyCars()
        {
            //      var i = _carRepository.GetAll().Where(o => o.IdOwner.Id == _userManager.GetUserId(HttpContext.User));

            //var j = _carRepository.GetAll();
            // var kl = HttpContext.User;
            // var k = (_userManager.GetUserAsync(HttpContext.User)).Id;

            // var o = User.Identity.GetUserId();
            // m
            // var ac = from t in j
            //          where t.IdOwner.Id == k.ToString()
            //          select t;

            // j.Select(o => o.IdOwner.Id == User.Identity.GetUserId());

            var cars = _carRepository.GetAll(_userManager.GetUserId(HttpContext.User));

            return View(cars);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _carRepository.Remove(id);
            await _carRepository.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
