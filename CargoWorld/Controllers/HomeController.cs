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

        public HomeController(UserManager<ApplicationUser> userManager,
            IRepository<ApplicationUser> userRepository)
        {
            _userManager = userManager;
            _userRepository = (UserRepository)userRepository;
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
            var user = _userRepository.Get(id);

            return View(user);
        }

    }
}
