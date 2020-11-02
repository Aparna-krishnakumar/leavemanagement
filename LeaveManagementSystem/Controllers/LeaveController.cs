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
using System.Net.Mail;
using System.Web.Configuration;
using System.Linq.Expressions;

namespace LeaveManagementSystem.Controllers
{
    [CustomAuthenticationFilter]
    public class LeaveController : Controller
    {
        IEmployeesService us;
        ILeaveDataService ls;
        ILeaveDataAdminService lsa;
        IProjectsService ps;
        LeaveManagementSystemDatabaseDbContext db;
        ILeaveData lr;
        ILeaveDataAdmin lra;

        public LeaveController(IEmployeesService us, ILeaveDataService ls, ILeaveDataAdminService lsa, IProjectsService ps)
        {
            this.us = us;
            this.ls = ls;
            this.lsa = lsa;
            this.ps = ps;
            db = new LeaveManagementSystemDatabaseDbContext();
            lr = new LeaveDataRepository();
            lra = new LeaveDataAdminRepository();
        }
        public ActionResult LeaveList()
        {
            List<LeaveDataViewModel> leavedata = ls.GetLeaveData();
            return View(leavedata);
        }
        public ActionResult LeaveListAdmin()
        {
            List<LeaveDataAdminViewModel> leavedataadmin = lsa.GetLeaveData();
            return View(leavedataadmin);
        }

        public ActionResult LeaveApprove(int id)
        {
            LeaveData l = db.LeaveDatas.Where(temp => temp.LeaveID == id).FirstOrDefault();
            l.Approved = true;
            l.Rejected = false;
            lr.ResponseToLeaveData(l);
            Employee u = db.Employees.Where(temp => temp.EmployeeID == l.EmployeeID).FirstOrDefault();
            SmtpClient smtp = new SmtpClient(WebConfigurationManager.AppSettings["Server"]);
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["From"], WebConfigurationManager.AppSettings["Password"]);
            smtp.Send(WebConfigurationManager.AppSettings["From"], u.Email,
               "Leave Approved", "Your leave application for leave of absence from " + l.StartDate.ToShortDateString() + " to " + l.EndDate.ToShortDateString() + " is approved.");
            return RedirectToAction("LeaveList", "Leave");

        }
        public ActionResult LeaveApproveAdmin(int id)
        {
            LeaveDataAdmin la = db.LeaveDataAdmins.Where(temp => temp.LeaveID == id).FirstOrDefault();
            la.Approved = true;
            la.Rejected = false;
            lra.ResponseToLeaveData(la);
            Employee u = db.Employees.Where(temp => temp.EmployeeID == la.EmployeeID).FirstOrDefault();
            SmtpClient smtp = new SmtpClient(WebConfigurationManager.AppSettings["Server"]);
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["From"], WebConfigurationManager.AppSettings["Password"]);
            smtp.Send(WebConfigurationManager.AppSettings["From"], u.Email,
               "Leave Approved", "Your leave application for leave of absence from " + la.StartDate.ToShortDateString() + " to " + la.EndDate.ToShortDateString() + " is approved.");
            return RedirectToAction("LeaveListAdmin", "Leave");

        }
        public ActionResult LeaveReject(int id)
        {
            LeaveData l = db.LeaveDatas.Where(temp => temp.LeaveID == id).FirstOrDefault();
            l.Approved = false;
            l.Rejected = true;
            lr.ResponseToLeaveData(l);
            Employee u = db.Employees.Where(temp => temp.EmployeeID == l.EmployeeID).FirstOrDefault();
            SmtpClient smtp = new SmtpClient(WebConfigurationManager.AppSettings["Server"]);
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["From"], WebConfigurationManager.AppSettings["Password"]);
            smtp.Send(WebConfigurationManager.AppSettings["From"], u.Email,
               "Leave Rejected", "Your leave application for leave of absence from " + l.StartDate.ToShortDateString() + " to " + l.EndDate.ToShortDateString() + " is rejected.");
            return RedirectToAction("LeaveList", "Leave");

        }
        public ActionResult LeaveRejectAdmin(int id)
        {
            LeaveDataAdmin la = db.LeaveDataAdmins.Where(temp => temp.LeaveID == id).FirstOrDefault();
            la.Approved = false;
            la.Rejected = true;
            lra.ResponseToLeaveData(la);
            Employee u = db.Employees.Where(temp => temp.EmployeeID == la.EmployeeID).FirstOrDefault();
            SmtpClient smtp = new SmtpClient(WebConfigurationManager.AppSettings["Server"]);
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["From"], WebConfigurationManager.AppSettings["Password"]);
            smtp.Send(WebConfigurationManager.AppSettings["From"], u.Email,
               "Leave Rejected", "Your leave application for leave of absence from " + la.StartDate.ToShortDateString() + " to " + la.EndDate.ToShortDateString() + " is rejected.");
            return RedirectToAction("LeaveListAdmin", "Leave");

        }
        public ActionResult LeaveRemove(int id)
        {
            ls.DeleteLeaveData(id);
            return RedirectToAction("LeaveList", "Leave");
        }
        public ActionResult LeaveRemoveAdmin(int id)
        {
            lsa.DeleteLeaveData(id);
            return RedirectToAction("LeaveListAdmin", "Leave");
        }
        public ActionResult AddLeave()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddLeave(LeaveDataViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                rvm.EmployeeID = (int)Session["CurrentUserID"];
                rvm.EmployeeName = (string)Session["CurrentUserName"];
                rvm.Approved = false;
                rvm.Rejected = false;
                ls.InsertLeaveData(rvm);
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
        public ActionResult AddLeaveAdmin()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddLeaveAdmin(LeaveDataAdminViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                rvm.EmployeeID = (int)Session["CurrentUserID"];

                rvm.EmployeeName = (string)Session["CurrentUserName"];
                rvm.Approved = false;
                rvm.Rejected = false;
                lsa.InsertLeaveData(rvm);
                if((string)Session["CuurentuserDesignation"]=="HR")
                {
                    return RedirectToAction("Index", "HR");
                }
                else if((string)Session["CuurentuserDesignation"] == "Project Manager")
                {
                    return RedirectToAction("Index", "ProjectManager");
                }
                else
                {
                    return RedirectToAction("Index", "ProjectManager");
                }
               
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
    }
}