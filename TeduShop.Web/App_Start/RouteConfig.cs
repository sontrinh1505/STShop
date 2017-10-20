using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TeduShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Contact",
                url: "contact.html",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );


            routes.MapRoute(
                name: "Search",
                url: "search.html",
                defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login.html",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Register",
                url: "register.html",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Cart",
               url: "cart.html",
               defaults: new { controller = "ShoppingCart", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "TeduShop.Web.Controllers" }
           );

            routes.MapRoute(
              name: "Checkout",
              url: "checkout.html",
              defaults: new { controller = "ShoppingCart", action = "CheckOut", id = UrlParameter.Optional },
              namespaces: new string[] { "TeduShop.Web.Controllers" }
          );

            routes.MapRoute(
                name: "Page",
                url: "page/{alias}.html",
                defaults: new { controller = "Page", action = "Index", alias = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );


            routes.MapRoute(
                name: "Product Category",
                url: "{alias}.pc-{id}.html",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Product",
                url: "{alias}.p-{id}.html",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );


            routes.MapRoute(
                name: "Hot Product",
                url: "hot/product.html",
                defaults: new { controller = "Product", action = "HotProduct", id = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Latest Product",
               url: "latest/product.html",
               defaults: new { controller = "Product", action = "LatestProduct", id = UrlParameter.Optional },
               namespaces: new string[] { "TeduShop.Web.Controllers" }
           );


            routes.MapRoute(
                name: "TagList",
                url: "tag/{tagId}.html",
                defaults: new { controller = "Product", action = "ListByTag", tagId = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
