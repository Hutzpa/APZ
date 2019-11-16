using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.Models
{
    public class CargoInCar
    {
        [Key]
        public int Id_Delivery { get; set; }
        public Car Id_Car { get; set; }
        public List<Cargo> Id_Cargo { get; set; }
        public int AmountOfCarog { get; set; }
    }
}
