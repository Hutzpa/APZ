using CargoWorld.Data;
using CargoWorld.Data.FileManager;
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
        private CarRepository _carRepository;
        private UserManager<ApplicationUser> _userManager;
        private IFileManager _fileManager;

        public CarController(IRepository<Car> carRepository, 
            UserManager<ApplicationUser> userManager,
            IFileManager fileManager)
        {
            _carRepository = (CarRepository)carRepository;
            _userManager = userManager;
            _fileManager = fileManager;
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
                    IdCar = (int)car.IdCar,
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
                IdDriver = String.Empty,
                IdGroup = cvm.IdGroup,
                CarModel = cvm.CarModel,
                CarcassNumber = cvm.CarcassNumber,
                RegistrationNumber = cvm.RegistrationNumber,
                Photo = await _fileManager.SaveImage(cvm.Image),
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
            return View(_carRepository.Get(id));
        }


        [HttpGet]
        public IActionResult ImDriving()
        {
            var res = _carRepository.IAmDriving(_userManager.GetUserId(HttpContext.User));
            return View(res);
        }

        public async Task<IActionResult> DrivingRefuseAsync(int idCar)
        {
            await _carRepository.RefuseDrivingAsync(idCar);
            // refusing
            return RedirectToAction("ImDriving");
        }


        [HttpGet]
        public IActionResult MyCars(int pageNumber)
        {
            if (pageNumber < 1)
                return RedirectToAction("MyCars", new { pageNumber = 1 });

            var list = _carRepository.GetAll(_userManager.GetUserId(HttpContext.User), pageNumber);
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _carRepository.Remove(id);
            await _carRepository.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("/Image/{image}")]
        public IActionResult Image(string image)
        {
            var type = image.Substring(image.LastIndexOf('.')+1);
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{type}");
        }
    }
}
