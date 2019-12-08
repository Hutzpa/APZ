using CargoWorld.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.ViewModels
{
    public class CargoViewModel
    {
        public int Id_Cargo { get; set; }
        public ApplicationUser Id_Owner { get; set; }
        public CargoInCar Transfer { get; set; }
        public bool IsDelivered { get; set; }

        [Required(ErrorMessage = "Має бути заповнений")]
        [MinLength(2)]
        public string CargoName { get; set; }
        /// <summary>
        /// Точка отправки
        /// </summary>
        [Required(ErrorMessage = "Має бути заповнений")]

        [MinLength(2)]
        public string DeparturePoint { get; set; }
        /// <summary>
        /// Точка доставки
        /// </summary>
        [Required(ErrorMessage = "Має бути заповнений")]
        [MinLength(2)]
        public string DestinationPoint { get; set; }
        public string Photo { get; set; }
        /// <summary>
        /// Вес в кг
        /// </summary>
        [Required]
        [Range(0d, 50000d)]
        public int Weight { get; set; }
        /// <summary>
        /// Какой груз? Жидкий\сыпучий\доски\бетонные блоки 
        /// </summary>
        public string CargoType { get; set; }

        [Required]
        [Range(0d, 500d)]
        public double Height { get; set; }
        [Required]
        [Range(0d, 50000d)]
        public double Width { get; set; }
        [Required]
        [Range(0d, 50000d)]
        public double Length { get; set; }
        /// <summary>
        /// Может ли груз быть разделён на несколько частей?
        /// </summary>
        public bool CanBeSepateted { get; set; }
        /// <summary>
        /// Объём
        /// </summary>

        public double Bulk { get; set; }


        public IFormFile Image { get; set; } = null;

        public int? idGroup { get; set; }
    }
}
