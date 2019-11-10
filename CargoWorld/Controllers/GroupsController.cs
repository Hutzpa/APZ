using CargoWorld.Data;
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
    public class GroupsController : Controller
    {
        private GroupRepository _groupsRepository;

        public GroupsController(IRepository<Group> groupsRepository)
        {
            _groupsRepository = (GroupRepository)groupsRepository;
        }

        [HttpGet]
        public IActionResult AGroup(Group group)
        {
            return View(group);
        }
        
        
        [HttpGet]
        public IActionResult GroupList()
        {
            return View(_groupsRepository.GetAll());
        }


        [HttpGet]
        public IActionResult CreateGroup(int? id)
        {

            if (id == null)
                return View(new GroupViewModel {
                    Cars = _groupsRepository.GetCarsWithoutGroup()
                }) ;
            else
            {
                var group = _groupsRepository.Get((int)id);
                return View(new GroupViewModel
                {
                    IdGroup = group.IdGroup,
                    IdOwner = group.IdOwner,
                    GroupName = group.GroupName,
                    Cars = _groupsRepository.GetCarsWithoutGroup()
                });
            }
        }

    }
}
