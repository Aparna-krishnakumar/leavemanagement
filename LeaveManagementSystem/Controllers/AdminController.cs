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
    public class AdminController : Controller
    {
        IEmployeesService us;
        ILeaveDataService ls;
        IProjectsService ps;
        LeaveManagementSystemDatabaseDbContext db;
        ILeaveData lr;
        IRoleService rs;

        public AdminController(IEmployeesService us, ILeaveDataService ls, IProjectsService ps, IRoleService rs)
        {
            this.us = us;
            this.ls = ls;
            this.ps = ps;
            this.rs = rs;
            db = new LeaveManagementSystemDatabaseDbContext();
            lr = new LeaveDataRepository();
        }
        [CustomAuthorize("Admin")]
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProjectList()
        {
            List<ProjectViewModel> projectdata = ps.GetProjects();
            return View(projectdata);
        }
        public ActionResult EmployeeList()
        {
            SearchViewModel employeeData = new SearchViewModel();
            var names = us.GetEmployees();
            employeeData.employeeList = names;
            employeeData.Names = us.GetEmployeeSelectListItem(names);
            var roles = rs.GetRoles();
            employeeData.Roles = rs.GetRoleSelectListItem(roles);
            return View(employeeData);
        }
        public ActionResult Search(string Name, string Role)
        {
            SearchViewModel employeeData = new SearchViewModel();
            var names = us.GetEmployees();
            employeeData.Names = us.GetEmployeeSelectListItem(names);
            var roles = rs.GetRoles();
            employeeData.Roles = rs.GetRoleSelectListItem(roles);
            if (Name != "" && Role != "")
            {
                employeeData.employeeList = us.GetEmployees().Where(temp => temp.Name.ToLower().Contains(Name.ToLower()) && temp.Designation.ToLower().Contains(Role.ToLower())).ToList();
                return View("EmployeeList", employeeData);
            }
            else if (Name != "" && Role == "")
            {
                employeeData.employeeList = us.GetEmployees().Where(temp => temp.Name.ToLower().Contains(Name.ToLower())).ToList();
                return View("EmployeeList", employeeData);
            }
            else
            {
                employeeData.employeeList = us.GetEmployees().Where(temp => temp.Designation.ToLower().Contains(Role.ToLower())).ToList();
                return View("EmployeeList", employeeData);
            }
        }
        public ActionResult EmployeeRemove(int id)
        {
            us.DeleteEmployee(id);
            return RedirectToAction("EmployeeList", "Admin");
        }

        public ActionResult AddEmployeeHR()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddEmployeeHR(EmployeeViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];
                    byte[] imgBytes = new byte[file.ContentLength + 1];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    rvm.Photo = base64String;
                }
                rvm.Designation = "HR";
                int uid = this.us.InsertEmployee(rvm);


                return RedirectToAction("EmployeeList", "Admin");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
        public ActionResult AddEmployeeProjectManager()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddEmployeeProjectManager(EmployeeViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];
                    byte[] imgBytes = new byte[file.ContentLength + 1];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    rvm.Photo = base64String;
                }
                rvm.Designation = "Project Manager";
                int uid = this.us.InsertEmployee(rvm);


                return RedirectToAction("EmployeeList", "Admin");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
        public ActionResult SpecialPermission(int id)
        {
            Employee e = db.Employees.Where(temp => temp.EmployeeID == id).FirstOrDefault();
            e.IsSpecialPermission = true;
            db.SaveChanges();
            return RedirectToAction("EmployeeList", "Admin");
        }
        public ActionResult UpdateEmployee(int id)
        {
            
            EmployeeViewModel uvm = this.us.GetEmployeesByEmployeeID(id);
            EditEmployeeDetailsViewModel eudvm = new EditEmployeeDetailsViewModel() { Name = uvm.Name, Email = uvm.Email, Mobile = uvm.Mobile, EmployeeID = uvm.EmployeeID };
            return View(eudvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEmployee(EditEmployeeDetailsViewModel eudvm)
        {
            if (ModelState.IsValid)
            {
                this.us.UpdateEmployeeDetails(eudvm);
                return RedirectToAction("EmployeeList", "Admin");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(eudvm);
            }
        }

    }
}