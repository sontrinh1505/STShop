using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Full name can not be blank")]
        public string FullName { set; get; }

        [Required(ErrorMessage = "User name can not be blank")]
        public string UserName { set; get; }

        
        [Required(ErrorMessage = "PassWord can not be blank")]
        [MinLength(6, ErrorMessage = "PassWord must be great than 6 characters")]     
        public string PassWord { set; get; }

        [EmailAddress(ErrorMessage = "incorrect email")]
        [Required(ErrorMessage = "Email can not be blank")]
        public string Email { set; get; }

        public string Address { set; get; }

        [Required(ErrorMessage = "Phone number can not be blank")]
        public string PhoneNumber { set; get; }
    }
}