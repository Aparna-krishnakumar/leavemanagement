using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LeaveManagementSystem.DomainModels;
using System.Web.Mvc;
using System.Web.Routing;

namespace LeaveManagementSystem.Infrastructure
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        public CustomAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            
            if (httpContext.Session["CurrentUserID"] != null)
                using (var context = new LeaveManagementSystemDatabaseDbContext())
                {
                    int userId = (int)httpContext.Session["CurrentUserID"];
                    var userRole = (from u in context.Employees
                                    join r in context.Roles on u.Designation equals r.RoleName
                                    where u.EmployeeID == userId
                                    select new
                                    {
                                        r.RoleName
                                    }).FirstOrDefault();
                    foreach (var role in allowedroles)
                    {
                        if (role == userRole.RoleName) return true;
                    }
                }


            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Account" },
                    { "action", "Login" }
               });
        }
    }
}