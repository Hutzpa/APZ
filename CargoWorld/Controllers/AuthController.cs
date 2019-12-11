using CargoWorld.Models;
using CargoWorld.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AuthController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
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
            var res = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);
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
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var User = new ApplicationUser
            {
                UserName = vm.Email,
                Email = vm.Email,
                Name = vm.Name,
                Surname = vm.Surname,
                Age = vm.Age
            };
            var result = await _userManager.CreateAsync(User, vm.Password);


            if (result.Succeeded)
            {
                Registration(User.Email, User.Name);
                await _signInManager.SignInAsync(User, false);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public IActionResult ChangePass()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassAsync(RegisterViewModel rvm)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            await ResetAsync(user.Email, rvm.Password);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Reset()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ResetAsync(RegisterViewModel rvm)
        {
            if (!ModelState.IsValid)
            {
                return View(rvm);
            }
            await ResetAsync(rvm.Email);
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> DeleteAsync(string email, int pageNumber)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("AdminPanel", "Home",new { pageNumber});
                }
                return RedirectToAction("AdminPanel", "Home", new { pageNumber });
            }
            return RedirectToAction("Index", "Home");
        }

        private void Registration(string email, string name)
        {
            try
            {
                // отправитель - устанавливаем адрес и отображаемое в письме имя
                MailAddress from = new MailAddress("illia.bezuhlyi@nure.ua", "Cargo world");
                // кому отправляем
                MailAddress to = new MailAddress(email);

                // создаем объект сообщения
                MailMessage m = new MailMessage(from, to);
                // тема письма
                m.Subject = "Welcome";
                // текст письма
                m.Body = name + " welcome in cargo world service";
                // письмо представляет код html
                m.IsBodyHtml = true;
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                // логин и пароль
                smtp.Credentials = new NetworkCredential("illia.bezuhlyi@nure.ua", "TLnK5nd3");
                smtp.EnableSsl = true;
                smtp.Send(m);

            }
            catch (Exception ex)
            {

            }
        }

        private async Task ResetAsync(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user != null)
            {
                string newPass = "FEgrkogbb" + (new Random()).Next(0, 9999) + "_forgot" + (new Random()).Next(99, 333);
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                try
                {
                    // отправитель - устанавливаем адрес и отображаемое в письме имя
                    MailAddress from = new MailAddress("illia.bezuhlyi@nure.ua", "Cargo world");
                    // кому отправляем
                    MailAddress to = new MailAddress(email);

                    // создаем объект сообщения
                    MailMessage m = new MailMessage(from, to);
                    // тема письма
                    m.Subject = "Password restoring";
                    // текст письма
                    m.Body = "New passport is : " + newPass;
                    // письмо представляет код html
                    m.IsBodyHtml = true;
                    // адрес smtp-сервера и порт, с которого будем отправлять письмо
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                    // логин и пароль
                    smtp.Credentials = new NetworkCredential("illia.bezuhlyi@nure.ua", "TLnK5nd3");
                    smtp.EnableSsl = true;
                    smtp.Send(m);

                    var res = await _userManager.ResetPasswordAsync(user, code, newPass);


                }
                catch (Exception ex)
                {

                }
            }
        }
        private async Task ResetAsync(string email, string pass)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user != null)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                try
                {
                    // отправитель - устанавливаем адрес и отображаемое в письме имя
                    MailAddress from = new MailAddress("illia.bezuhlyi@nure.ua", "Cargo world");
                    // кому отправляем
                    MailAddress to = new MailAddress(email);

                    // создаем объект сообщения
                    MailMessage m = new MailMessage(from, to);
                    // тема письма
                    m.Subject = "Welcome";
                    // текст письма
                    m.Body = "The password was succesfully changed";
                    // письмо представляет код html
                    m.IsBodyHtml = true;
                    // адрес smtp-сервера и порт, с которого будем отправлять письмо
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                    // логин и пароль
                    smtp.Credentials = new NetworkCredential("illia.bezuhlyi@nure.ua", "TLnK5nd3");
                    smtp.EnableSsl = true;
                    smtp.Send(m);

                    var res = await _userManager.ResetPasswordAsync(user, code, pass);


                }
                catch (Exception ex)
                {

                }
            }
        }

    }
}
