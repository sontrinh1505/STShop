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

namespace TeduShop.Web.Customs
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        protected override bool IsAuthorized(HttpActionContext httpContext)
        {
            var context = new TeduShopDbContext();

            bool authorize = false;
            var userId = httpContext.RequestContext.Principal.Identity.GetUserId();

            var listGroupId = context.ApplicationUserGroups
                .Where(x => x.UserId == userId)
                
                .Select(x => x.GroupId);
            

            var listPermission = context.ApplicationPermissionGroups.Where(x => listGroupId.Contains(x.GroupId)).Select(x => x.ApplicationPermission);
           
            var controller = httpContext.ControllerContext.ControllerDescriptor.ControllerName;

            return authorize;
        }


        //public override void OnAuthorization(HttpActionContext httpContext)
        //{
        //    var controller = httpContext.ControllerContext.ControllerDescriptor.ControllerName;

             
        //    base.OnAuthorization(httpContext);

        //}

      




       



        public ApplicationUser GetUser(string userId)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TeduShopDbContext()));
            ApplicationUser user = manager.Users.FirstOrDefault(x => x.Id == userId);
            return user;
        }
    }
}