
using CargoWorld.Data;
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
    public class RequestController : Controller
    {
        private RequestRepository _requestManager;
        private UserManager<ApplicationUser> _userManager;
        private UserRepository _userRepository;

        public RequestController(IRepository<Request> requestManager,
            UserManager<ApplicationUser> userManager,
            IRepository<ApplicationUser> userRepository)
        {
            _requestManager = (RequestRepository)requestManager;
            _userManager = userManager;
            _userRepository = (UserRepository)userRepository;
        }


        #region Company to user
        [HttpGet]
        public IActionResult CompanyToUser()
        {
            var requests = _requestManager.SelectRequests(RequestType.CompanyOffersToUser, _userManager.GetUserId(HttpContext.User));
            return View(requests);
        }

        //Пользователь предлагает компании перевозку груза
        public async Task<IActionResult> AcceptCompanyToUserAsync(int requestId)
        {
            var request = _requestManager.Get(requestId);

            await _requestManager.AcceptCompanyToUserAsync((int)request.IdCargo, (int)request.IdGroup, _userManager.GetUserId(HttpContext.User));

            return RedirectToAction("CompanyToUser");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanyToUserAsync(CargoViewModel cvm)
        {
            string f = cvm.Id_Owner.Id;
            Models.Request request = new Request()
            {
                Recip = _userRepository.Get(cvm.Id_Owner.Id),
                RequestType = RequestType.CompanyOffersToUser,
                Name = "Вам предложили перевозку груза",
                Description = "Вам предложили перевозку груза",
                IdGroup = cvm.idGroup,
                IdCargo = cvm.Id_Cargo
            };
            _requestManager.Create(request);
            await _requestManager.SaveChangesAsync();
            return RedirectToAction("DrivingRequest");
        }

        #endregion

        #region Driving

        [HttpGet]
        public IActionResult DrivingRequest()
        {
            var requests = _requestManager.SelectRequests(RequestType.DrivingRequest, _userManager.GetUserId(HttpContext.User));
            return View(requests);
        }

        public async Task<IActionResult> AcceptDrivingAsync(int requestId)
        {
            // присвоить id водителя машины, ид пользователя, а все входящие запросы на вождение для этого пользователя удалить

            var request = _requestManager.Get(requestId);

            await _requestManager.AcceptDrivingAsync((int)request.IdCar, _userManager.GetUserId(HttpContext.User));
            return RedirectToAction("DrivingRequest");
        }

        [HttpPost]
        public async Task<IActionResult> CreateDrivingRequestAsync(UserViewModel uvm)
        {
            Models.Request request = new Request()
            {
                IdCar = uvm.IdCarWithoutDriver,
                Recip = _userRepository.Get(uvm.ApplicationUser.Id),
                RequestType = RequestType.DrivingRequest,
                Name = "Вас запрошують працювати водієм",
                Description = "Вас запрошують працювати водієм"

            };
            _requestManager.Create(request);
            await _requestManager.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region User to company

        public async Task<IActionResult> CreateUserToCompanyAsync(UserViewModel uvm)
        {
            Models.Request request = new Request()
            {
                Recip = _userRepository.Get(uvm.ApplicationUser.Id),
                IdCargo = uvm.IdCargo,
                RequestType = RequestType.UserOffersToCompany,
                Name = "Вам пропонують перевезти вантаж",
                Description = "Вам пропонують перевезти вантаж"
            };
            _requestManager.Create(request);
            await _requestManager.SaveChangesAsync();
            return RedirectToAction("DrivingRequest");

        }


        [HttpGet]
        public IActionResult UserToCompany()
        {
            //здеся проверять на всю 
            var requests = _requestManager.SelectRequests(RequestType.UserOffersToCompany, _userManager.GetUserId(HttpContext.User));
            return View(requests);
        }

        public async Task<IActionResult> AcceptUserToCompanyAsync(int requestId)
        {
            // есть два варика, сформировать групу под груз, или выбрать из списка групу из существующих куда пихать(груз добавляется в cargoin the car)

          
            var request = _requestManager.Get(requestId);
            await _requestManager.AcceptUserToCompany((int)request.IdCargo, (int)request.IdGroup, request.Id);
            return RedirectToAction("CompanyToUser");
        }




        #endregion

        public IActionResult DeclineRequest(int id, string sender)
        {
            _requestManager.Remove(id);
            return RedirectToAction(sender);
        }
    }
}
