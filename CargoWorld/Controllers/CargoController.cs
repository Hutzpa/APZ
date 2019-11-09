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
    public class CargoController : Controller
    {
        private CargoRepository _cargoRepository;

        public CargoController(CargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        [HttpGet]
        public IActionResult ACargo()
        {
            return View(new Cargo());
        }

        [HttpGet]
        public IActionResult CreateCargo(int? id)
        {
            if (id == null)
                return View(new Cargo());
            else
            {
                var cargo = _cargoRepository.Get((int)id);
                return View(new Cargo
                {
                    Id_Cargo = cargo.Id_Cargo,
                    Id_Owner = cargo.Id_Owner,
                    IsDelivered = cargo.IsDelivered,
                    CargoName = cargo.CargoName,
                    DeparturePoint = cargo.DeparturePoint,
                    DestinationPoint = cargo.DestinationPoint,
                    Photo = cargo.Photo,
                    Weight = cargo.Weight,
                    CargoType = cargo.CargoType,
                    Height = cargo.Height,
                    Width= cargo.Width,
                    Length  =  cargo.Length
                });
            }
        }

        public IActionResult CargoList()
        {
            var lst = _cargoRepository.GetAll();
            return View(lst);
        }
    }
}
