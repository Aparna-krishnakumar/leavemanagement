using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaveManagementSystem.DomainModels;
using LeaveManagementSystem.ServiceLayer;
using LeaveManagementSystem.ViewModels;

namespace LeaveManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        IEmployeesService us;

        public AccountController(IEmployeesService us)
        {
            this.us = us;
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
                    var imgBytes = new Byte[file.ContentLength - 1];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    rvm.Photo = base64String;
                }
                int uid = this.us.InsertEmployee(rvm);
                Session["CurrentUserID"] = uid;
                Session["CurrentUserName"] = rvm.Name;
                Session["CurrentUserEmail"] = rvm.Email;
                Session["CurrentUserPassword"] = rvm.Password;
               
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
        public ActionResult Login()
        {
            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }
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
                  

                  
                        return RedirectToAction("Index", "Home");
                    
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
            return RedirectToAction("Index", "Home");
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
    }
}