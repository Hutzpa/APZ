﻿using CargoWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.ViewModels
{
    public class OptimalGroupViewModel
    {
        public IEnumerable<Car> CarsThisGroup { get; set; }

        public Cargo Cargo { get; set; }
    }
}
