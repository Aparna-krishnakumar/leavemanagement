using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaveManagementSystem.Infrastructure;
namespace LeaveManagementSystem.Controllers
{
    public class ProjectManagerController : Controller
    {
        [CustomAuthenticationFilter]
        // GET: ProjectManager
        [CustomAuthorize("Project Manager")]
        public ActionResult Index()
        {
            return View();
        }
    }
}