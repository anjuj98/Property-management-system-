using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace Property_rental_management_system.Models
{
    public class property
    {
        public int propertyId { get; set; }

        [DisplayName("Property type")]
        [Required(ErrorMessage = "Property type is required")]


        public string property_type { get; set; }

        [DisplayName("Property name")]

        public string property_name { get; set; }

        [DisplayName("Available for")]
        [Required(ErrorMessage = "Available for required")]


        public string available_for { get; set; }


        [DisplayName("Available from")]
        [Required(ErrorMessage = "Available date is required")]

        [DataType(DataType.Date)]

        public string available_from { get; set; }

        [Required(ErrorMessage = "Rent amount details is required")]

        [DisplayName("Rent amount")]

        public string rent_amount { get; set; }

        [DisplayName("Rent type")]
        [Required(ErrorMessage = "Rent type is required")]


        public string rent_type { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Address is required")]


        public string address { get; set; }

        [DisplayName("State")]
        [Required(ErrorMessage = "Please select state")]


        public string state { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "Please select city")]


        public string city { get; set; }

        [DisplayName("Phone number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits")]

        public string phone_number { get; set; }


        [DisplayName("Property image")]
        public string property_image { get; set; }


        [NotMapped]
        public HttpPostedFileBase Imagefile { get; set; }

        [DisplayName("Property description")]
        [Required(ErrorMessage = "Property description is required")]


        public string property_description { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "Current status is required")]

        public string status { get; set; }


        [DisplayName("Message")]
        public string Messages { get; set; }



    }
}