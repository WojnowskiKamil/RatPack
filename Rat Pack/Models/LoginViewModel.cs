using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rat_Pack.Models
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Nazwa użytkownika")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Hasło")]
        public string Password { get; set; }

        [DisplayName("Zapamiętaj mnie")]
        public bool RememberMe { get; set; }
    }
}