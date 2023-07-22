using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Property_rental_management_system.Models
{
    public class Messages
    {
        public int Id { get; set; }

        [DisplayName("Full name")]
        [Required(ErrorMessage = "First name is required")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Only alphabetic characters are allowed")]

        public string Full_name { get; set; }

        [DisplayName("Email address")]
        [Required(ErrorMessage = "Email address is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [DisplayName("Phone number")]
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits")]
        public string Phone_number { get; set; }

        [DisplayName("Message")]
        [Required(ErrorMessage = "Message is required")]

        public string Message { get; set; }

    }
}