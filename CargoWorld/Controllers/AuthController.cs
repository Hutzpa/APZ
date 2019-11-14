using CargoWorld.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public AuthController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var res = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password,false,false);
            if (!res.Succeeded)
            {
                return View(vm);
            }
                return RedirectToAction("Index", "Home");
         
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            var User = new IdentityUser
            {
                UserName = vm.Email,
                Email = vm.Email
            };
            var result = await _userManager.CreateAsync(User, vm.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(User,false);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
