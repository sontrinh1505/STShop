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
            return true;
            //var context = new TeduShopDbContext();
            bool authorize = false;
            if (!httpContext.RequestContext.Principal.Identity.IsAuthenticated)
                return false;
          

            var userId = httpContext.RequestContext.Principal.Identity.GetUserId();

            if (string.IsNullOrEmpty(userId))
                return false;

            var listGroupId = context.ApplicationUserGroups.Where(x => x.UserId == userId).Select(x => x.GroupId);

            var listPermission = context.ApplicationPermissionGroups.Where(x => listGroupId.Contains(x.GroupId)).Select(x => x.ApplicationPermission).ToList();

            var permissionIds = listPermission.Select(x => x.ID).ToList();

            var listRole = context.ApplicationRolePermissions.Where(x => permissionIds.Contains(x.PermissonId)).Select(x => x.ApplicationRole);

            var controllerName = httpContext.ControllerContext.ControllerDescriptor.ControllerName;

            authorize = listPermission.Any(x => x.ControllerName.Contains(controllerName));

            if (!authorize)
            {
                return false;
            }

            if (string.IsNullOrEmpty(Roles))
                return true;

            if (!listRole.Any(x => x.Name == Roles))
            {
                return false;
            }

           // return true; 
        }


        //public override void OnAuthorization(HttpActionContext httpContext)
        //{
        //    var controller = httpContext.ControllerContext.ControllerDescriptor.ControllerName;

             
        //    base.OnAuthorization(httpContext);

        //}

    }
}