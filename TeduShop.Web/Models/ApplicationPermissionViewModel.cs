using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class ApplicationPermissionViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Description { set; get; }

        public string ControllerName { set; get; }

        // public IEnumerable<ApplicationGroupViewModel> Groups { set; get; }

        public virtual IEnumerable<ApplicationRoleViewModel> Roles { set; get; }
    }
}