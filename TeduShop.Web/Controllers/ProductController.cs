using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TeduShop.Common;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class ProductController : Controller
    {
      
        IProductService _productService;
        IProductCategoryService _productCategoryService;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
        }


        public ActionResult Detail(int id)
        {
            var productModel = _productService.GetById(id);
            var productViewModel = Mapper.Map<Product, ProductViewModel>(productModel);
            var relatedProduct = _productService.GetRelatedProducts(id, 6);
            ViewBag.relatedProduct = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProduct);

            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(productViewModel.MoreImages);
            ViewBag.listImages = listImages;

            ViewBag.tags = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(_productService.GetListTagByProductId(id));

            return View(productViewModel);
        }

        public ActionResult Category(int id, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var ProductModel = _productService.GetListProductByCategoryIdPaging(id, page, pageSize, sort, out totalRow);
            var ProductModelViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(ProductModel);
            int totalPage = (int) Math.Ceiling((double)totalRow/pageSize);
            var category = _productCategoryService.GetById(id);

            ViewBag.category = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);


            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = ProductModelViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage,
            };         
            return View(paginationSet);
        }

        public ActionResult Search(string keyWord, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var ProductModel = _productService.Search(keyWord, page, pageSize, sort, out totalRow);
            var ProductModelViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(ProductModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.KeyWord = keyWord;
            
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = ProductModelViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage,
            };
            return View(paginationSet);
        }

        public ActionResult ListByTag(string tagId, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var ProductModel = _productService.GetListTProductByTag(tagId, page, pageSize, out totalRow);
            var ProductModelViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(ProductModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.tag = Mapper.Map<Tag, TagViewModel>(_productService.GetTag(tagId));

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = ProductModelViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage,
            };
            return View(paginationSet);
        }


        public JsonResult GetListProductByName(string keyWord)
        {
            var model = _productService.GetListProductByName(keyWord);
           
            return Json(new {
                data = model
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HotProduct()
        {
            var products = _productService.GetTopSale(20).ToListViewModel();

            return View(products);
        }

        public ActionResult LatestProduct()
        {
            var products = _productService.GetLastest(20).ToListViewModel();

            return View(products);
        }


    }
}