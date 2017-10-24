using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using TeduShop.Model.Models;

namespace TeduShop.Web.Models
{
    
    public class MenuGroupViewModel
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public int? ParentId { get; set; }

        public virtual IEnumerable<Menu> Menus { set; get; }
        public virtual IEnumerable<MenuGroup> ChildrenGroupMenus { set; get; }

        public virtual MenuGroup MenuGroups { get; set; }
    }
}