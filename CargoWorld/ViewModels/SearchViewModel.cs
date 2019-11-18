using CargoWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<Cargo> Cargos { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
