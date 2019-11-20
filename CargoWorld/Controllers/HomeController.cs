using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CargoWorld.Models;
using Microsoft.AspNetCore.Identity;
using CargoWorld.Data;
using CargoWorld.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using CargoWorld.ViewModels;

namespace CargoWorld.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private UserRepository _userRepository;
        private CarRepository _carRepository;

        public HomeController(UserManager<ApplicationUser> userManager,
            IRepository<ApplicationUser> userRepository,
            IRepository<Car> carRepository)
        {
            _userManager = userManager;
            _userRepository = (UserRepository)userRepository;
            _carRepository = (CarRepository)carRepository;
            
        }

        public IActionResult Index()
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            return View();
        }

        public IActionResult Search(string search)
        {
            SearchViewModel svm = _userRepository.Search(search);
            return View(svm);
        }

        [Authorize]
        public IActionResult AUser(string id)
        {
            //Пользователь на чью форму заходят
            var user = _userRepository.Get(id);

            var curentUser = _userRepository.Get(_userManager.GetUserId(HttpContext.User));
            ViewBag.CurUserId = curentUser.Id;
            //Получение машин без водителя
            ViewBag.CarsWithoutDriver = _carRepository.GetAll(_userManager.GetUserId(HttpContext.User)).ToList();


            UserViewModel uvm = new UserViewModel
            {
                ApplicationUser = user
            };

            return View(uvm);
        }

    }
}
