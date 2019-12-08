using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.Models
{
    public class Group
    {
        [Key]
        public int IdGroup { get; set; }

        public ApplicationUser IdOwner { get; set; }

        [Required]
        [MinLength(3)]
        public string GroupName { get; set; }

        public List<Car> Cars { get; set; }
    }
}
