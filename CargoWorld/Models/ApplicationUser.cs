using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string UserRole { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        /// <summary>
        /// Женщина\мужчина
        /// </summary>
        public string Sex { get; set; }
        public string Geoposition { get; set; }
    }
}
