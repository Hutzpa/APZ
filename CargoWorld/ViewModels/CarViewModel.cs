using CargoWorld.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.ViewModels
{
    public class CarViewModel
    {
        public int IdCar { get; set; }

        public int IdDriver { get; set; }

        public List<Group> IdGroup { get; set; }
        public ApplicationUser IdOwner { get; set; }

        public string CarModel { get; set; }
        public string CarcassNumber { get; set; }
        public string RegistrationNumber { get; set; }
        public string Photo { get; set; }
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
        public int CarryingCapacity { get; set; }
        /// <summary>
        /// объём грузового отделения
        /// </summary>
        public double CarryingCapacitySqM { get; set; }

        /// <summary>
        /// Высота грузового отделения
        /// </summary>
        public double HeightCargoCompartment { get; set; }
        /// <summary>
        /// Ширина грузового отделения
        /// </summary>
        public double WidthCargoCompartment { get; set; }
        /// <summary>
        /// Длина грузового отделения
        /// </summary>
        public double LengthCargoCompartment { get; set; }

        /// <summary>
        /// Цена перевозки груза на км
        /// </summary>
        public decimal CostPerKm { get; set; }

        public IFormFile Image { get; set; } = null;
    }
}
