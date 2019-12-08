using CargoWorld.Data;
using CargoWorld.Data.Repositories;
using CargoWorld.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.ViewModels
{
    public class GroupViewModel
    {
        public int IdGroup { get; set; }

        public ApplicationUser IdOwner { get; set; }

        [Required(ErrorMessage = "Назва має бути не пустою")]
        [MinLength(3)]
        public string GroupName { get; set; }

        public IEnumerable<Car> Cars { get; set; }
        
    }
       
}
