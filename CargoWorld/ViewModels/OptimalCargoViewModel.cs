using CargoWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.ViewModels
{
    public class OptimalCargoViewModel
    {
        public IEnumerable<Cargo> Cargos { get; set; }


        public CargoViewModel cvm { get; set; }
        public Group OurGroup { get; set; }
    }
}
