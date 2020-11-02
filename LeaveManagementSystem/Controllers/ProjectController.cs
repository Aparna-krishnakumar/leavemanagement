using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaveManagementSystem.DomainModels;
using LeaveManagementSystem.ServiceLayer;
using LeaveManagementSystem.ViewModels;
using LeaveManagementSystem.Infrastructure;
using LeaveManagementSystem.Repositories;

namespace LeaveManagementSystem.Controllers
{
    [CustomAuthenticationFilter]
    public class ProjectController : Controller
    {
        IProjectsService ps;
        public ProjectController(IProjectsService ps)
        {
            this.ps = ps;
        }
       
        public ActionResult AddProject()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddProject(ProjectViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                ps.InsertProject(rvm);
                return RedirectToAction("ProjectList", "Admin");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
        public ActionResult ProjectRemove(int id)
        {
            ps.DeleteProject(id);
            return RedirectToAction("ProjectList", "Admin");
        }
        public ActionResult UpdateProject(int id)
        {
            ProjectViewModel uvm = this.ps.GetProjectsByProjectID(id);
            return View(uvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProject(ProjectViewModel eudvm)
        {
            if (ModelState.IsValid)
            {
                this.ps.UpdateProjectDetails(eudvm);
                return RedirectToAction("ProjectList", "Admin");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(eudvm);
            }
        }

    }
}