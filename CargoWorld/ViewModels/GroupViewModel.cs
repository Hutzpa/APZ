using CargoWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.ViewModels
{
    public class GroupViewModel
    {
        public int IdGroup { get; set; }

        public List<User> IdOwner { get; set; }


        private IEnumerable<Car> carsInGroup;

        public IEnumerable<Car> CarsInGroup{
            get
            {
               return 
            }
    }
}
