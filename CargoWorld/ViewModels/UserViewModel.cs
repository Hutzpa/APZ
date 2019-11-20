using CargoWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.ViewModels
{
    public class UserViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }

        public int IdCarWithoutDriver { get; set; }
    }
}
