using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaveManagementSystem.DomainModels;
using LeaveManagementSystem.ServiceLayer;
using LeaveManagementSystem.ViewModels;
using LeaveManagementSystem.Infrastructure;

namespace LeaveManagementSystem.Controllers
{
    [CustomAuthenticationFilter]
    public class HRController : Controller
    {
        // GET: HR
        [CustomAuthorize("HR")]

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HRSpecialPermission()
        {
            return View();
        }
    }
}