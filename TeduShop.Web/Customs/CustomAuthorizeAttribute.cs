using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using TeduShop.Model.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using TeduShop.Data;
using TeduShop.Web.Infrastructure.Extensions;
using System.Web.Http.Controllers;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;

namespace TeduShop.Web.Customs
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        

        TeduShopDbContext context = new TeduShopDbContext();
        protected override bool IsAuthorized(HttpActionContext httpContext)
        {
            //return true;
            //var context = new TeduShopDbContext();
            bool authorize = false;
            if (!httpContext.RequestContext.Principal.Identity.IsAuthenticated)
                return false;

            var controllerName = httpContext.ControllerContext.ControllerDescriptor.ControllerName;

            var userId = httpContext.RequestContext.Principal.Identity.GetUserId();

            if (string.IsNullOrEmpty(userId))
                return false;

            if (string.IsNullOrEmpty(Roles))
                return true;

            var listGroupId = context.ApplicationUserGroups.Where(x => x.UserId == userId).Select(x => x.GroupId).ToList();

            if (listGroupId == null)
                return false;


            var permission = context.ApplicationPermissionGroups.Where(x => listGroupId.Contains(x.GroupId))
                .Select(x => x.ApplicationPermission)
                .FirstOrDefault(x => x.ControllerName == controllerName);

            authorize = context.ApplicationRolePermissions.Where(x => permission.ID == x.PermissonId && listGroupId.Contains(x.GroupId)).Select(x => x.ApplicationRole).Distinct().Any(x => x.Name == Roles);


            if (!authorize)
            {
                return false;
            }


            return true; 
        }


        //public override void OnAuthorization(HttpActionContext httpContext)
        //{
        //    var controller = httpContext.ControllerContext.ControllerDescriptor.ControllerName;

             
        //    base.OnAuthorization(httpContext);

        //}

    }
}