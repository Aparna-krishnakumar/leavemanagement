using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagementSystem.DomainModels;

namespace LeaveManagementSystem.Repositories
{

    public interface IEmployeeProjects
    {
        void InsertEmployeeProject(EmployeeProjectDetail ep);

        List<EmployeeProjectDetail> GetEmployeeProjectsByEmployeeID(int EmployeeID);
        void DeleteEmployeeProject(int epid, int pid);
        List<Project> GetEmployeeProjectsByProjectID(int ProjectID);
    }

    public class EmployeeProjectsRepository:IEmployeeProjects
    {
        LeaveManagementSystemDatabaseDbContext db;
        public EmployeeProjectsRepository()
        {
            db = new LeaveManagementSystemDatabaseDbContext();
        }
        public void InsertEmployeeProject(EmployeeProjectDetail ep)
        {
            db.EmployeeProjectDetails.Add(ep);
            db.SaveChanges();
        }
        public List<EmployeeProjectDetail> GetEmployeeProjectsByEmployeeID(int EmployeeID)
        {
            List<EmployeeProjectDetail> us = db.EmployeeProjectDetails.Where(temp => temp.EmployeeID == EmployeeID).ToList();
            return us;
        }
        public List<EmployeeProjectDetail> GetEmployeeProjectsByProjectID(int ProjectID)
        {
            List<EmployeeProjectDetail> us = db.EmployeeProjectDetails.Where(temp => temp.ProjectID == ProjectID).ToList();
            return us;
        }
        public void DeleteEmployeeProject(int epid, int pid)
        {
            List<EmployeeProjectDetail> us = db.EmployeeProjectDetails.Where(temp => temp.EmployeeID == epid).ToList();
            foreach(EmployeeProjectDetail e in us)
            {
                if (e.ProjectID==pid)
                {
                    db.EmployeeProjectDetails.Remove(e);
                    db.SaveChanges();
                }
            }
        }

        List<Project> IEmployeeProjects.GetEmployeeProjectsByProjectID(int ProjectID)
        {
            throw new NotImplementedException();
        }
    }
    }

