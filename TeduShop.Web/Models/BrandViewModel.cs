using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using TeduShop.Model.Models;

namespace TeduShop.Web.Models
{
    
    public class BrandViewModel
    {     
        public int ID { set; get; }

        public string Name { set; get; }

        public string Country { set; get; }

        public string Description { set; get; }

        public virtual ICollection<Product> Products { set; get; }
        public virtual ICollection<ModelBrand> ModelBrands { set; get; }
    }
}