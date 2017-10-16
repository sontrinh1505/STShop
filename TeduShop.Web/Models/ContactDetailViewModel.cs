using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class ContactDetailViewModel
    {
        public int ID { set; get; }

        [Required(ErrorMessage ="Name could not be blank.")]
        public string Name { set; get; }

        [MaxLength(50,ErrorMessage = "Phone Number could not be great than 50 characters")]
        public string Phone { set; get; }

        [MaxLength(250, ErrorMessage = "Email can could be great than 250 characters")]
        public string Email { set; get; }

        [MaxLength(250, ErrorMessage = "Website could not be great than 250 characters")]
        public string Website { set; get; }

        [MaxLength(250, ErrorMessage = "Address could not be great than 250 characters")]
        public string Address { set; get; }

        public string Others { set; get; }

        public double? Lat { set; get; }

        public double? Lng { set; get; }

        public bool Status { set; get; }
    }
}