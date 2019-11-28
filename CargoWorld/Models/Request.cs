using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.Models
{
    public enum RequestType
    {
        UserOffersToCompany,
        CompanyOffersToUser,
        DrivingRequest
    }
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Accepted { get; set; }

        /// <summary>
        /// Реквесты на вождение
        /// </summary>
        public int? IdCar { get; set; }
        /// <summary>
        /// Реквесты на "компания предлагает перевозку" 1\2
        /// </summary>
        public int? IdGroup { get; set; }
        /// <summary>
        /// Компании предлагают перевезти груз 2\2
        /// </summary>
        public int? IdCargo { get; set; }

        public RequestType RequestType { get; set; }

      
       // public string SenderId { get; set; }

        public ApplicationUser Recip { get; set; }
    }
}
