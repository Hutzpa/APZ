using CargoWorld.Data;
using CargoWorld.Data.Repositories;
using CargoWorld.Models;
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
        private IRepository<Group> _groupsRepository;

        public GroupsController(IRepository<Group> groupsRepository)
        {
            _groupsRepository = groupsRepository;
        }

        [HttpGet]
        public IActionResult AGroup()
        {
            return View(new Group());
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
                return View(new Group());
            else
            {
                var group = _groupsRepository.Get((int)id);
                return View(new Group
                {
                    IdGroup = group.IdGroup,
                    IdOwner = group.IdOwner
                });
            }
        }

    }
}
