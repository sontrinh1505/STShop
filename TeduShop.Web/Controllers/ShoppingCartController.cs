using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TeduShop.Common;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.App_Start;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        IProductService _productService;
        IOrderService _orderService;
        ApplicationUserManager _userManager;
        public ShoppingCartController(IProductService productService, IOrderService orderService, ApplicationUserManager userManager)
        {
            this._productService = productService;
            this._orderService = orderService;
            this._userManager = userManager;
        }


        public ActionResult Index()
        {
            if(Session[ComomConstants.SessionCart] == null)
                Session[ComomConstants.SessionCart] = new List<ShoppingCartViewModel>();

            return View();
        }

        public ActionResult CheckOut()
        {
            if (Session[ComomConstants.SessionCart] == null)
            {
                return Redirect("/cart.html");
            }

            return View();
        }

        public JsonResult GetUser()
        {
            if(Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = _userManager.FindById(userId);
                return Json(new {
                    data = user,
                    status = true
                });

            }
            return Json(new
            {
                status = false
            });

        }

        public JsonResult CreateOrder(string orderViewModel)
        {
            var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderViewModel);
            //var newOrder = new Order();
            var newOrder = order.ToModel();
            bool isEnough = true;
            //newOrder.UpdateOrder(order);
            if(Request.IsAuthenticated)
            {
                newOrder.CustomerId = User.Identity.GetUserId();
                newOrder.CreatedBy = User.Identity.GetUserName();
            }

            var cart = (List<ShoppingCartViewModel>)Session[ComomConstants.SessionCart];
            List<OrderDetail> orderDetail = new List<OrderDetail>();

            foreach(var item in cart)
            {
                var detail = new OrderDetail();
                detail.ProductID = item.ProductId;
                detail.Quantity = item.Quantity;
                detail.Price = item.Product.Price;
                orderDetail.Add(detail);
                isEnough = _productService.SellProduct(item.ProductId, item.Quantity);
                break;
            }
            if(isEnough)
            {
                _orderService.Create(newOrder, orderDetail);
                _productService.Save();
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false,
                    message = "not enough Product in stock"
                });
            }
           
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            if (Session[ComomConstants.SessionCart] == null)
                Session[ComomConstants.SessionCart] = new List<ShoppingCartViewModel>();

            var cart = (List<ShoppingCartViewModel>)Session[ComomConstants.SessionCart];
            return Json(new {
                status = true,
                data = cart
            },JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Add(int productId)
        {
            var cart = (List<ShoppingCartViewModel>)Session[ComomConstants.SessionCart];
            var product = _productService.GetById(productId);
            if(cart == null)
                cart = new List<ShoppingCartViewModel>();

            if(product.Quantity == 0)
            {
                return Json(new
                {
                    status = false,
                    message = "not enough product in stock"
                });
            }

            if (cart.Any(x => x.ProductId == productId))
            {
                foreach (var item in cart)
                {
                    if (item.ProductId == productId)
                    {
                        item.Quantity += 1;
                    }
                }
            }
            else
            {
                ShoppingCartViewModel newItem = new ShoppingCartViewModel();
                newItem.ProductId = productId;
                newItem.Product = Mapper.Map<Product, ProductViewModel>(product);
                newItem.Quantity = 1;
                cart.Add(newItem);
            }

            Session[ComomConstants.SessionCart] = cart;
            return Json(new {
                status = true
            });
        }

        [HttpPost]
        public JsonResult Update(string cartData)
        {
            var cartViewModel = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(cartData);
            var cartSession = (List<ShoppingCartViewModel>)Session[ComomConstants.SessionCart];

            foreach(var item in cartSession)
            {
                foreach(var jitem in cartViewModel)
                {
                    if(item.ProductId == jitem.ProductId)
                    {
                        item.Quantity = jitem.Quantity;
                    }
                }
            }
            Session[ComomConstants.SessionCart] = cartSession;

            return Json(new {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteItem(int productId)
        {
            var cartSession = (List<ShoppingCartViewModel>)Session[ComomConstants.SessionCart];
            var status = false;
            if(cartSession != null)
            {
                cartSession.RemoveAll(x => x.ProductId == productId);
                Session[ComomConstants.SessionCart] = cartSession;
                status = true;
            }
            return Json(new {
                status = status
            });
        }


        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[ComomConstants.SessionCart] = new List<ShoppingCartViewModel>();

            return Json(new
            {
                status = true
            });
        }
        
    }
}