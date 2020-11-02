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
    public class EmployeeController : Controller
    {
        // GET: Employee
        [CustomAuthorize("Employee")]
        public ActionResult Index()
        {
            return View();
        }
    }
}