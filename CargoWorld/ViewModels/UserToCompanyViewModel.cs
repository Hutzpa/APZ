using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.ViewModels
{
    public class UserToCompanyViewModel
    {
        public List<RequestViewModel> Requests { get; set; }

        public int? IdRequest { get; set; }

        public int? IdGroup { get; set; }
    }
}
