using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class FeedbackViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name could not be blank")]
        [MaxLength(250, ErrorMessage = "Name could not be great than 250 characters")]
        public string Name { set; get; }

        [MaxLength(250, ErrorMessage = "Email could not be great than 250 characters")]
        public string Email { set; get; }

        [MaxLength(500, ErrorMessage = "Message could not be great than 500 characters")]
        public string Message { set; get; }

        public DateTime CreatedDate { set; get; }

        [Required(ErrorMessage = "Status could not be blank")]
        public bool Status { set; get; }

        public ContactDetailViewModel ContactDetail { set; get; }
    }
}