using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Remoting.Lifetime;

namespace Property_rental_management_system.Models
{
    public class Signin
    {
        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }


        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]

        public string Password { get; set; }


       



    }
}