using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Property_rental_management_system.Models
{
    public class Signup
    {
       
        public int Id { get; set; }


        [DisplayName("First name")]
        [Required(ErrorMessage ="First name is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        public string Firstname { get; set; }


        [DisplayName("Last name")]
        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed")]
        public string Lastname { get; set; }


        [DisplayName("Date of birth")]
        [DataType(DataType.Date)] 
        [Required(ErrorMessage = "Date of birth is required")]
        public string Dateofbirth { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }


        [DisplayName("Phone number")]
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits")]
        public string Phonenumber { get; set; }


        [DisplayName("Email address")]
        [Required(ErrorMessage = "Email address is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }


        [DisplayName("Address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [DisplayName("State")]
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [DisplayName("Pincode")]
        [Required(ErrorMessage = "Pincode is required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pincode must be a 6-digit number")]
        public string Pincode { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter valid email format")]
        public string Username { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and be at least 8 characters long.")]
        public string Password { get; set; }

        [DisplayName("Confirm password")]
        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string Confirmpassword { get; set; }


    }
}