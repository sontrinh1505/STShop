using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Common;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        ICommonService _commonService;
        IProductService _productService;
        IMenuService _menuService;
        IMenuGroupService _menuGroupService;

        public HomeController(IProductCategoryService productCategoryService, 
            ICommonService commonService, IProductService productService,
            IMenuService menuService, IMenuGroupService menuGroupService
            )
        {
            this._productCategoryService = productCategoryService;
            this._commonService = commonService;
            this._productService = productService;
            this._menuService = menuService;
            this._menuGroupService = menuGroupService;
        }

        [OutputCache(Duration = 3600, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            var slideModel = _commonService.GetSlides();           
            var lastestProductModel = _productService.GetLastest(10);
            var topSaleProductModel = _productService.GetHotProduct(10);

            var slideModelViewModel = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
            var lastestProductModelViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProductModel);
            var topSaleProductModelViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProductModel);

            var homeViewModel = new HomeViewModel();
            homeViewModel.Slides = slideModelViewModel;
            homeViewModel.LastestProducts = lastestProductModelViewModel;
            homeViewModel.TopSaleProducts = topSaleProductModelViewModel;
            try
            {
                homeViewModel.Title = _commonService.GetSystemConfig(ComomConstants.HomeTitle).ValueString;
                homeViewModel.MetaKeyword = _commonService.GetSystemConfig(ComomConstants.HomeMetaKeyword).ValueString;
                homeViewModel.MetaDescription = _commonService.GetSystemConfig(ComomConstants.HomeMataDescription).ValueString;
            }
            catch
            {

            }
           
            return View("~/Views/Home/Index_NewTemplate.cshtml", homeViewModel);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Footer()
        {
            //var footerModel = _commonService.GetFooter();
            //var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footerModel);
            return PartialView("~/Views/Shared/Footer_NewTemplate.cshtml");
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            var homeViewModel = new HomeViewModel();
            string[] include = { "Menus"};
            var menus = _menuGroupService.GetAll().ToList();
            
                homeViewModel.Menus = menus.ToListViewModel();

            return PartialView("~/Views/Shared/Header_NewTemplate.cshtml", homeViewModel);
        }

        [ChildActionOnly]
        public ActionResult TopNavigation()
        {
            return PartialView("~/Views/Shared/TopNavigation_NewTemplate.cshtml");
        }

        [ChildActionOnly]
        public ActionResult Banner()
        {
            return PartialView("~/Views/Shared/Banner_NewTemplate.cshtml");
        }

        [ChildActionOnly]
        public ActionResult ProductTab()
        {
            return PartialView("~/Views/Shared/ProductTab_NewTemplate.cshtml");
        }

        [ChildActionOnly]
        public ActionResult BestSeller()
        {
            return PartialView("~/Views/Shared/BestSeller_NewTemplate.cshtml");
        }

        [ChildActionOnly]
        public ActionResult RecentView()
        {
            return PartialView("~/Views/Shared/RecentView_NewTemplate.cshtml");
        }

        [ChildActionOnly]
        public ActionResult Topbrand()
        {
            return PartialView("~/Views/Shared/TopBrand_NewTemplate.cshtml");
        }



        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Category()
        {
            var model = _productCategoryService.GetAll();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable< ProductCategoryViewModel>>(model);
            return PartialView(listProductCategoryViewModel);
        }
    }
}