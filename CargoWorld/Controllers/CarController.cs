using CargoWorld.Data;
using CargoWorld.Data.Repositories;
using CargoWorld.Models;
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
                return View(new Car());
            else
            {
                var car = _carRepository.Get((int)id);
                return View(new Car
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

        [Obsolete("Передавать машину")]
        [HttpGet]
        public IActionResult ACar()
        {
            return View(new Car());
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


    }
}
