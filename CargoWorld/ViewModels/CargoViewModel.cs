﻿using CargoWorld.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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

        public string CargoName { get; set; }
        /// <summary>
        /// Точка отправки
        /// </summary>
        public string DeparturePoint { get; set; }
        /// <summary>
        /// Точка доставки
        /// </summary>
        public string DestinationPoint { get; set; }

        public string Photo { get; set; }
        /// <summary>
        /// Вес в кг
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// Какой груз? Жидкий\сыпучий\доски\бетонные блоки 
        /// </summary>
        public string CargoType { get; set; }

        public double Height { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }

        public IFormFile Image { get; set; } = null;

        public int? idGroup { get; set; }
    }
}
