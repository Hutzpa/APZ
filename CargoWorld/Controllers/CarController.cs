using CargoWorld.Data;
using CargoWorld.Data.Repositories;
using CargoWorld.Models;
using CargoWorld.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.Controllers
{
    public class CarController : Controller
    {
        private IRepository<Car> _carRepository;

        public CarController(IRepository<Car> carRepository )
        {
            _carRepository = carRepository;
        }

        [HttpGet]
        public IActionResult CreateCar(int? id)
        {
            if (id == null)
                return View(new CarViewModel());
            else
            {
                var car = _carRepository.Get((int)id);
                return View(new CarViewModel
                {
                    IdCar = car.IdCar,
                    IdDriver = car.IdDriver,
                    IdGroup = car.IdGroup,
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
                }) ;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(CarViewModel cvm)
        {
            var car = new Car
            {
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

        [Obsolete("Передавать машину")]
        public IActionResult ACar(CarViewModel cvm)
        {
            return View(cvm);
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
            return View( _carRepository.GetAll());
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
