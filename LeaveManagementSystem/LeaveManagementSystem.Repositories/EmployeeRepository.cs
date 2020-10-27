using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using LeaveManagementSystem.DomainModels;

namespace LeaveManagementSystem.Repositories
{
   
        public interface IEmployees
        {
            void InsertEmployee(Employee e);
            void UpdateEmployeeDetails(Employee e);
            void UpdateEmployeePassword(Employee e);
            void DeleteEmployee(int eid);
            List<Employee> GetEmployees();
            List<Employee> GetEmployeesByEmailAndPassword(string Email, string PasswordHash);
            List<Employee> GetEmployeesByEmail(string Email);

            List<Employee> GetEmployeesByEmployeeID(int EmployeeID);
            int GetLatestEmployeeID();

        }
        public class EmployeesRepository : IEmployees
        {
            LeaveManagementSystemDatabaseDbContext db;
            public EmployeesRepository()
            {
                db = new LeaveManagementSystemDatabaseDbContext();
            }
            public void InsertEmployee(Employee e)
            {
            
                db.Employees.Add(e);
                db.SaveChanges();
            }

            public void UpdateEmployeeDetails(Employee e)
            {
                Employee us = db.Employees.Where(temp => temp.EmployeeID == e.EmployeeID).FirstOrDefault();
                if (us != null)
                {
                    us.Name = e.Name;
                    us.Address = e.Address;
                    us.Mobile = e.Mobile;
                us.Photo = e.Photo;
                    db.SaveChanges();
                }
            }

            public void UpdateEmployeePassword(Employee e)
            {
                Employee us = db.Employees.Where(temp => temp.EmployeeID == e.EmployeeID).FirstOrDefault();
                if (us != null)
                {
                    us.PasswordHash = e.PasswordHash;
                    db.SaveChanges();
                }
            }

            public void DeleteEmployee(int eid)
            {
                Employee us = db.Employees.Where(temp => temp.EmployeeID == eid).FirstOrDefault();
                if (us != null)
                {
                    db.Employees.Remove(us);
                    db.SaveChanges();
                }
            }

            public List<Employee> GetEmployees()
            {
                List<Employee> us = db.Employees.OrderBy(temp => temp.Name).ToList();
                return us;
            }

            public List<Employee> GetEmployeesByEmailAndPassword(string Email, string PasswordHash)
            {
                List<Employee> us = db.Employees.Where(temp => temp.Email == Email && temp.PasswordHash == PasswordHash).ToList();
                return us;
            }

            public List<Employee> GetEmployeesByEmail(string Email)
            {
                List<Employee> us = db.Employees.Where(temp => temp.Email == Email).ToList();
                return us;
            }

            public List<Employee> GetEmployeesByEmployeeID(int EmployeeID)
            {
                List<Employee> us = db.Employees.Where(temp => temp.EmployeeID == EmployeeID).ToList();
                return us;
            }

            public int GetLatestEmployeeID()
            {
                int uid = db.Employees.Select(temp => temp.EmployeeID).Max();
                return uid;
            }
        }
    
}
