using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.Models
{
    public class CargoInCar
    {
        [Key]
        public int Id_Delivery { get; set; }
       
        public Car Transporter { get; set; }
        public Cargo Cargo { get; set; }
        public int AmountOfCarog { get; set; }
    }
}
