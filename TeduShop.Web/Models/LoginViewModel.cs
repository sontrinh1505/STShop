using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "you need to enter user name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "you need to enter password")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        public bool RememberMe { get; set; }
    }
}