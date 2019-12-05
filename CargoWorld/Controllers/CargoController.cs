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
    public class CargoController : Controller
    {
        private CargoRepository _cargoRepository;
        private UserManager<ApplicationUser> _userManager;
        private IFileManager _fileManager;
        private GroupRepository _groupRepository;

        public CargoController(IRepository<Cargo> cargoRepository,
            UserManager<ApplicationUser> userManager,
            IRepository<Group> groupRepository,
            IFileManager fileManager)
        {
            _cargoRepository = (CargoRepository)cargoRepository;
            _userManager = userManager;
            _fileManager = fileManager;
            _groupRepository = (GroupRepository)groupRepository;
        }

        [HttpGet]
        public IActionResult ACargo(int id)
        {
            var cargo = _cargoRepository.Get(id);
            CargoViewModel cvm = new CargoViewModel
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
                Width = cargo.Width,
                Length = cargo.Length,
                CanBeSepateted = cargo.CanBeSepateted,
                Bulk = cargo.Bulk


            };
            var grps = _groupRepository.GetAll(_userManager.GetUserId(HttpContext.User))
                .Where(o=>o.Cars.FirstOrDefault(o=>o.CargoType == cvm.CargoType) != null);
            ViewBag.GroupsToOffer = grps;
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
                    Width = cargo.Width,
                    Length = cargo.Length,
                    CanBeSepateted = cargo.CanBeSepateted,
                    Bulk = cargo.Bulk

                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargo(CargoViewModel cvm)
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User));
            var cargo = new Cargo
            {
                Id_Cargo = cvm.Id_Cargo,
                Id_Owner = user,
                IsDelivered = cvm.IsDelivered,
                CargoName = cvm.CargoName,
                DeparturePoint = cvm.DeparturePoint,
                DestinationPoint = cvm.DestinationPoint,
                Photo = await _fileManager.SaveImage(cvm.Image),
                Weight = cvm.Weight,
                CargoType = cvm.CargoType,
                Height = cvm.Height,
                Width = cvm.Width,
                Length = cvm.Length,
                CanBeSepateted = cvm.CanBeSepateted,
                Bulk = cvm.Bulk


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

        public IActionResult CargoList(int pageNumber)
        {
            if (pageNumber < 1)
                return RedirectToAction("CargoList", new { pageNumber = 1 });

            var list = _cargoRepository.GetAll(_userManager.GetUserId(HttpContext.User), pageNumber);
            return View(list);
        }

        [HttpGet("/Image/{image}")]
        public IActionResult Image(string image)
        {
            var type = image.Substring(image.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{type}");
        }


        [HttpGet]
        public IActionResult IMTransporting()
        {
            var cargos = _cargoRepository.CargosImTransporting(_userManager.GetUserId(HttpContext.User));
            return View(cargos);
        }

        [Obsolete("Не сделано")]
        public async Task<IActionResult> SetDeliveredAsync(int idCargo)
        {
            Cargo cr = _cargoRepository.Get(idCargo);
            cr.IsDelivered = true;
            _cargoRepository.Update(cr);
            await _cargoRepository.SaveChangesAsync();
            return RedirectToAction("IMTransporting");
        }

        public IActionResult OptimalGroup(int idCargo)
        {

            var ogv = new OptimalGroupViewModel
            {
                CarsThisGroup = _groupRepository.CreateGroupForCargo(idCargo, _userManager.GetUserId(HttpContext.User)),
                Cargo = _cargoRepository.Get(idCargo)
            };
            return View(ogv);
        }
    }
}
