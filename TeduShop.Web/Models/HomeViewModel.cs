using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeduShop.Model.Models;

namespace TeduShop.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<SlideViewModel> Slides { set; get; }
        public IEnumerable<ProductViewModel> LastestProducts { set; get; }
        public IEnumerable<ProductViewModel> TopSaleProducts { set; get; }
        public IEnumerable<ProductViewModel> BestSelleProduct { set; get; }
        public IEnumerable<ProductViewModel>Hotroducts { set; get; }
        public IEnumerable<MenuGroupViewModel> Menus { set; get; }
        public IEnumerable<ProductCategoryViewModel> ProductCategories { set; get; }
        
        public string Title { set; get; }
        public string MetaKeyword { set; get; }
        public string MetaDescription { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
    }
}