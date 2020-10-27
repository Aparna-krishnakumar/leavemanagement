using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagementSystem.DomainModels;


namespace LeaveManagementSystem.Repositories
{
    
        public interface IProjects
        {
            void InsertProject(Project p);
            void UpdateProjectDetails(Project p);
            void DeleteProject(int pid);
            List<Project> GetProjects();

            List<Project> GetProjectsByProjectID(int ProjectID);
            int GetLatestProjectID();

        }
        public class ProjectsRepository : IProjects
        {
            LeaveManagementSystemDatabaseDbContext db;
            public ProjectsRepository()
            {
                db = new LeaveManagementSystemDatabaseDbContext();
            }
            public void InsertProject(Project p)
            {
                db.Projects.Add(p);
                db.SaveChanges();
            }

            public void UpdateProjectDetails(Project p)
            {
                Project us = db.Projects.Where(temp => temp.ProjectID == p.ProjectID).FirstOrDefault();
                if (us != null)
                {
                    us.ProjectName = p.ProjectName;
                    us.ProjectManager = p.ProjectManager;
                    db.SaveChanges();
                }
            }
            public void DeleteProject(int pid)
            {
                Project us = db.Projects.Where(temp => temp.ProjectID == pid).FirstOrDefault();
                if (us != null)
                {
                    db.Projects.Remove(us);
                    db.SaveChanges();
                }
            }
            public List<Project> GetProjects()
            {
                List<Project> us = db.Projects.ToList();
                return us;
            }
            public List<Project> GetProjectsByProjectID(int ProjectID)
            {
                List<Project> us = db.Projects.Where(temp => temp.ProjectID == ProjectID).ToList();
                return us;
            }

            public int GetLatestProjectID()
            {
                int uid = db.Projects.Select(temp => temp.ProjectID).Max();
                return uid;
            }

        }
    
}
