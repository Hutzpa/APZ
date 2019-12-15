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
    public class CarViewModel
    {
        public int IdCar { get; set; }

        public string IdDriver { get; set; }

        public Group IdGroup { get; set; }
        public List<CargoInCar> CargoInThisCar { get; set; }
        public ApplicationUser IdOwner { get; set; }

        public string CarModel { get; set; }
        [Required]
        [StringLength(17,MinimumLength =11,ErrorMessage ="VIN має бути довжиной від 11 до 17 символів")]

        public string CarcassNumber { get; set; }

        [Required]
        [StringLength(13,MinimumLength = 4,ErrorMessage = "Введіть номер автомобілю")]
        public string RegistrationNumber { get; set; }

        public string Photo { get; set; }
        [Required]
        public string Color { get; set; }
        /// <summary>
        /// Тип, грузовая, малогруз(газелька), фура
        /// </summary>
        public string CarType { get; set; }
        /// <summary>
        /// Тип перевозимого груза (бензин\воду\зерно) 
        /// </summary>
        public string CargoType { get; set; }
        /// <summary>
        /// Грузоподъёмность в кг
        /// </summary>
        [Required]
        [Range(0d, 500d)]
        public int CarryingCapacity { get; set; }
        /// <summary>
        /// объём грузового отделения
        /// </summary>
        [Required]
        [Range(0d, 500d)]
        public double CarryingCapacitySqM { get; set; }
        /// <summary>
        /// Высота грузового отделения
        /// </summary>
        [Required]
        [Range(0d, 500d)]
        public double HeightCargoCompartment { get; set; }
        /// <summary>
        /// Ширина грузового отделения
        /// </summary>
        [Required]
        [Range(0d, 500d)]
        public double WidthCargoCompartment { get; set; }
        /// <summary>
        /// Длина грузового отделения
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Range(0d, 500d)]
        public double LengthCargoCompartment { get; set; }

        /// <summary>
        /// Цена перевозки груза на км
        /// </summary>
        [Required]
        [Range(0d, 50000d)]
        public decimal CostPerKm { get; set; }
        [Required(AllowEmptyStrings =false)]
        public IFormFile Image { get; set; } = null;
    }
}
