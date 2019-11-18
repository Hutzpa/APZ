using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email{ get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password{ get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword{ get; set; }
        [StringLength(50,MinimumLength = 2,ErrorMessage ="Длина должна быть от 2 до 50 символов")]
        [Required(ErrorMessage = "Обязательное поле, при регистрации аккаунта компании, фамилия не обязательна")]
        public string Name { get; set; }
        [StringLength(20,ErrorMessage = "Максимальная длина 20 символов")]
        public string Surname { get; set; }
        [Range(18,100,ErrorMessage = "Сервис доступен только для людей старше 18 лет и младше 100")]
        public int Age { get; set; }

    }
}
