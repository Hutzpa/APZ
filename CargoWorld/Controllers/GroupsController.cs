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
    public class GroupsController : Controller
    {
        private GroupRepository _groupsRepository;
        private UserManager<ApplicationUser> _userManager;

        public GroupsController(IRepository<Group> groupsRepository, UserManager<ApplicationUser> userManager)
        {
            _groupsRepository = (GroupRepository)groupsRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult AGroup(Group group)
        {
            return View(group);
        }


        [HttpGet]
        public IActionResult GroupList()
        {
            var grps = _groupsRepository.GetAll(_userManager.GetUserId(HttpContext.User));
            return View(grps);
        }


        [HttpGet]
        public IActionResult CreateGroup(int? id)
        {

            if (id == null)
                return View(new GroupViewModel
                {
                    Cars = _groupsRepository.GetCarsWithoutGroup()
                });
            else
            {
                var group = _groupsRepository.Get((int)id);
                var cars = _groupsRepository.GetAll(_userManager.GetUserId(HttpContext.User),"I'm ashamed tbh");
                return View(new GroupViewModel
                {
                    IdGroup = group.IdGroup,
                    IdOwner = group.IdOwner,
                    GroupName = group.GroupName,
                    Cars = cars,
                   
                });
            }
        }



        [HttpPost]
        public async Task<IActionResult> CreateGroupAsync(GroupViewModel gvm)
        {
            List<ApplicationUser> ownr = new List<ApplicationUser>{
               await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User))
            };
            var group = new Group
            {
                IdGroup = gvm.IdGroup,
                IdOwner = ownr,
                GroupName = gvm.GroupName
                
            };
            //какого-то фига не сохраняет
            bool f = _groupsRepository.SaveChangesAsync().GetAwaiter().GetResult();
             if (f)
                return RedirectToAction("Index", "Home");
            else
                return View(group);
        }

    }
}
