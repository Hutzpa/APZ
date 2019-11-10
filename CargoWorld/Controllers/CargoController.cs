﻿using CargoWorld.Data;
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
    public class CargoController : Controller
    {
        private IRepository<Cargo> _cargoRepository;

        public CargoController(IRepository<Cargo> cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        [HttpGet]
        public IActionResult ACargo(CargoViewModel cvm)
        {
            return View(cvm);
        }

        [HttpGet]
        public IActionResult CreateCargo(int? id)
        {
            if (id == null)
                return View(new CargoViewModel());
            else
            {
                var cargo = _cargoRepository.Get((int)id);
                return View(new CargoViewModel
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

        [HttpPost]
        public async Task<IActionResult> CreateCargo(CargoViewModel cvm)
        {
            var cargo = new Cargo
            {
                Id_Cargo = cvm.Id_Cargo,
                Id_Owner = cvm.Id_Owner,
                IsDelivered = cvm.IsDelivered,
                CargoName = cvm.CargoName,
                DeparturePoint = cvm.DeparturePoint,
                DestinationPoint = cvm.DestinationPoint,
                Photo = cvm.Photo,
                Weight = cvm.Weight,
                CargoType = cvm.CargoType,
                Height = cvm.Height,
                Width = cvm.Width,
                Length = cvm.Length
            };

            if (cvm.Id_Cargo > 0)
                _cargoRepository.Update(cargo);
            else
                _cargoRepository.Create(cargo);

            if (await _cargoRepository.SaveChangesAsync())
                return RedirectToAction("Index", "Home");
            else
                return View(cvm);
                    
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _cargoRepository.Remove(id);
            await _cargoRepository.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CargoList()
        {
            var lst = _cargoRepository.GetAll();
            return View(lst);
        }
    }
}
