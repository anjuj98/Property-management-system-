using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Property_rental_management_system.Models
{
    public class passwordChange
    {
        [DisplayName("Old password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter your old password")]

        public string OldPassword { get; set; }

        [DisplayName("New password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter your new password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit, one special character, and be at least 8 characters long.")]

        public string NewPassword { get; set; }

        [DisplayName("Confirm new password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please confirm your new password")]
        [Compare("NewPassword", ErrorMessage = "New password and confirmation new password do not match.")]


        public string ConfirmPassword { get; set; }
    }
}