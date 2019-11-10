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

        public List<User> IdOwner { get; set; }


        public string GroupName { get; set; }
    }
}
