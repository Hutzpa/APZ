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
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace CargoWorld.Controllers
{
    
    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private UserRepository _userRepository;
        private CarRepository _carRepository;
        private CargoRepository _cargoRepository;

        public HomeController(UserManager<ApplicationUser> userManager,
            IRepository<ApplicationUser> userRepository,
            IRepository<Car> carRepository,
            IRepository<Cargo> cargoRepository)
        {
            _userManager = userManager;
            _userRepository = (UserRepository)userRepository;
            _carRepository = (CarRepository)carRepository;
            _cargoRepository = (CargoRepository)cargoRepository;
            
        }

        public IActionResult Index()
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            return View();
        }

        public IActionResult Search(string search)
        {
            SearchViewModel svm = _userRepository.Search(search, _userManager.GetUserId(HttpContext.User));
            return View(svm);
        }

        [Authorize]
        public IActionResult AUser(string id)
        {
            //Пользователь на чью форму заходят
            var user = _userRepository.Get(id);

            var curentUser = _userRepository.Get(_userManager.GetUserId(HttpContext.User));
         
            //Получение машин без водителя
            ViewBag.CarsWithoutDriver = _carRepository.GetAll(_userManager.GetUserId(HttpContext.User)).ToList();

            ViewBag.UserCargos = _cargoRepository.CargosOfSpecUser(_userManager.GetUserId(HttpContext.User));

            ViewBag.CurUserId = _userManager.GetUserId(HttpContext.User);
            UserViewModel uvm = new UserViewModel
            {
                ApplicationUser = user
            };

            return View(uvm);
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTime.Now.AddYears(1) } );

            return LocalRedirect(returnUrl);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel(int pageNumber)
        {
            if(pageNumber < 1)
                return RedirectToAction("AdminPanel", new { pageNumber = 1 });

            ViewBag.Cargos = _cargoRepository.GetAll(pageNumber);

            ViewBag.Users = _userRepository.GetAll(pageNumber);

            ViewBag.CanNext = ViewBag.Cargos.CanNext == false ? ViewBag.Users.CanNext : ViewBag.Cargos.CanNext;

            ViewBag.PageNumber = pageNumber;

            return View();
        }
    }
}
