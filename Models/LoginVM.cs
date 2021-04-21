using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentRoom.Models
{
    public class LoginVM
    {

        [Required(ErrorMessage = "Enter login!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Enter password!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}