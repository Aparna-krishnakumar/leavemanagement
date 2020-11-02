using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using LeaveManagementSystem.DomainModels;
using LeaveManagementSystem.ServiceLayer;
using LeaveManagementSystem.ViewModels;
using LeaveManagementSystem.Infrastructure;
using LeaveManagementSystem.Repositories;
using System.Net.Mail;

namespace LeaveManagementSystem.Controllers
{
    [CustomAuthenticationFilter]
    public class AccountController : Controller
    {
        // GET: Account
        IEmployeesService us;
        ILeaveDataService ls;
        IProjectsService ps;
        IRoleService rs;
        IEmployeeProjectDetailsService eds;

        public AccountController(IEmployeesService us, ILeaveDataService ls, IProjectsService ps, IRoleService rs, IEmployeeProjectDetailsService eds)
        {
            this.us = us;
            this.ls = ls;
            this.ps = ps;
            this.rs = rs;
            this.eds =eds;
        }
        public ActionResult AddEmployeeProject()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AdddEmployeeProject(EditEmployeeProjectDetailsViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                eds.InsertEmployeeProject(rvm);
                return RedirectToAction("Index", "ProjectManager");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
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
        public ActionResult EmployeeRemove(int id)
        {
            us.DeleteEmployee(id);
            return RedirectToAction("EmployeeList", "Account");
        }

        public ActionResult AddEmployee()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddEmployee(EmployeeViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                if(Request.Files.Count>=1)
                {
                    var file = Request.Files[0];
                    byte[] imgBytes = new byte[file.ContentLength + 1];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    rvm.Photo = base64String;
                }
                int uid = this.us.InsertEmployee(rvm);
               
                return RedirectToAction("EmployeeList", "Account");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
       
        
        [OverrideAuthentication]
        public ActionResult Login()
        {
            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }
        [OverrideAuthentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                EmployeeViewModel uvm = this.us.GetEmployeesByEmailAndPassword(lvm.Email, lvm.Password);
                if (uvm != null)
                {
                    Session["CurrentUserID"] = uvm.EmployeeID;
                    Session["CurrentUserName"] = uvm.Name;
                    Session["CurrentUserEmail"] = uvm.Email;
                    Session["CurrentUserPassword"] = uvm.Password;

                    Session["CuurentuserDesignation"] = uvm.Designation;
                    Session["CurrentUserPermission"] = uvm.IsSpecialPermission;
                    if (uvm.Designation=="Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if(uvm.Designation == "HR")
                    {
                        if(uvm.IsSpecialPermission==false)
                        { return RedirectToAction("Index", "HR"); }
                        else
                        {
                            return RedirectToAction("HRSpecialPermission", "HR");
                        }
                        
                    }
                    else if(uvm.Designation=="Project Manager")
                    {
                        return RedirectToAction("Index", "ProjectManager");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Employee");
                    }
                }
                else
                {
                    ModelState.AddModelError("x", "Invalid Email / Password");
                    return View(lvm);
                }
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View(lvm);
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult ChangeProfile()
        {
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            EmployeeViewModel uvm = this.us.GetEmployeesByEmployeeID(uid);
            EditEmployeeDetailsViewModel eudvm = new EditEmployeeDetailsViewModel() { Name = uvm.Name, Email = uvm.Email, Mobile = uvm.Mobile, EmployeeID = uvm.EmployeeID };
            return View(eudvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeProfile(EditEmployeeDetailsViewModel eudvm)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];
                    var imgBytes = new Byte[file.ContentLength - 1];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    eudvm.Photo = base64String;
                }
                eudvm.EmployeeID = Convert.ToInt32(Session["CurrentUserID"]);
                this.us.UpdateEmployeeDetails(eudvm);
                Session["CurrentUserName"] = eudvm.Name;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(eudvm);
            }
        }

        public ActionResult ChangePassword()
        {
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            EmployeeViewModel uvm = this.us.GetEmployeesByEmployeeID(uid);
            EditEmployeePasswordViewModel eupvm = new EditEmployeePasswordViewModel() { Email = uvm.Email, Password = "", ConfirmPassword = "", EmployeeID = uvm.EmployeeID };
            return View(eupvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(EditEmployeePasswordViewModel eupvm)
        {
            if (ModelState.IsValid)
            {
                eupvm.EmployeeID = Convert.ToInt32(Session["CurrentUserID"]);
                this.us.UpdateEmployeePassword(eupvm);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(eupvm);
            }
        }
        public ActionResult Search(string Name, string Role)
        {
            SearchViewModel employeeData = new SearchViewModel();
            var names = us.GetEmployees();
            employeeData.Names = us.GetEmployeeSelectListItem(names);
            var roles = rs.GetRoles();
            employeeData.Roles = rs.GetRoleSelectListItem(roles);
            if (Name!="" && Role!="")
            {
                employeeData.employeeList =  us.GetEmployees().Where(temp => temp.Name.ToLower().Contains(Name.ToLower()) && temp.Designation.ToLower().Contains(Role.ToLower())).ToList();
                return View("EmployeeList",employeeData);
            }
           else if(Name!=""&& Role=="")
            {
                employeeData.employeeList =  us.GetEmployees().Where(temp => temp.Name.ToLower().Contains(Name.ToLower())).ToList();
                return View("EmployeeList",employeeData);
            }
            else
            {
                employeeData.employeeList =  us.GetEmployees().Where(temp => temp.Designation.ToLower().Contains(Role.ToLower())).ToList();
                return View("EmployeeList",employeeData);
            }    
        }

    }
}