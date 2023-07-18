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
        public string Full_name { get; set; }

        [DisplayName("Email address")]
        public string Email { get; set; }

        [DisplayName("Phone number")]
        public string Phone_number { get; set; }

        [DisplayName("Message")]
        public string Message { get; set; }

    }
}