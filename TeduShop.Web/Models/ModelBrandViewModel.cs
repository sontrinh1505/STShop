using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeduShop.Model.Models;

namespace TeduShop.Web.Models
{
    public class ModelBrandViewModel
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string Mode { set; get; }

        public string Description { set; get; }

        public int BrandID { set; get; }

        public virtual Brand Brand { set; get; }
    }
}