using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rat_Pack.Models
{
    public class RegisterViewModel
    {
        [Required]
        [DisplayName("Nazwa użytkownika")]
        public string Login { get; set; }

        [Required] [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Telefon")]
        public string Phone { get; set; }

        [Required]
        [DisplayName("Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DisplayName("Powtórz hasło")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [DisplayName("Chcę zarejestrować się jako dostawca")]
        public bool IsClientSender { get; set; }
    }
}