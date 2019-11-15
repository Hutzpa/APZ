using CargoWorld.Data;
using CargoWorld.Data.Repositories;
using CargoWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.ViewModels
{
    public class GroupViewModel
    {
        public int IdGroup { get; set; }

        public List<ApplicationUser> IdOwner { get; set; }

        public string GroupName { get; set; }


        public IEnumerable<Car> Cars { get; set; }

    }
       
}
