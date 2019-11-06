using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        //Админ, или регулярный пользователь
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
