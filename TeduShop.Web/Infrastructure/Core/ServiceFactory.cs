using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeduShop.Web.Infrastructure.Core
{
    public static class ServiceFactory
    {
        public static THeper Get<THeper>()
        {
            if(HttpContext.Current != null)
            {
                var key = string.Concat("factory-", typeof(THeper).Name);
                if(!HttpContext.Current.Items.Contains(key))
                {
                    var resolvedService = DependencyResolver.Current.GetService<THeper>();
                    HttpContext.Current.Items.Add(key, resolvedService);
                }
                return (THeper)HttpContext.Current.Items[key];
            }
            return DependencyResolver.Current.GetService<THeper>();
        }
    }
}