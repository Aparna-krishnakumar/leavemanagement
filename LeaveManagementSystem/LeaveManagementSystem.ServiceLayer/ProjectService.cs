using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagementSystem.DomainModels;
using LeaveManagementSystem.ViewModels;
using LeaveManagementSystem.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace LeaveManagementSystem.ServiceLayer
{
    public interface IProjectsService
    {
        void InsertProject(ProjectViewModel p);
        void UpdateProjectDetails(ProjectViewModel p);
        void DeleteProject(int pid);
        List<ProjectViewModel> GetProjects();

        ProjectViewModel GetProjectsByProjectID(int ProjectID);

    }
    public class ProjectsService : IProjectsService
    {
        IProjects cr;

        public ProjectsService()
        {
            cr = new ProjectsRepository();
        }

        public void InsertProject(ProjectViewModel cvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ProjectViewModel, Project>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Project c = mapper.Map<ProjectViewModel, Project>(cvm);
            cr.InsertProject(c);
        }

        public void UpdateProjectDetails(ProjectViewModel cvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ProjectViewModel, Project>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Project c = mapper.Map<ProjectViewModel, Project>(cvm);
            cr.UpdateProjectDetails(c);
        }

        public void DeleteProject(int cid)
        {
            cr.DeleteProject(cid);
        }

        public List<ProjectViewModel> GetProjects()
        {
            List<Project> c = cr.GetProjects();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Project, ProjectViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<ProjectViewModel> cvm = mapper.Map<List<Project>, List<ProjectViewModel>>(c);
            return cvm;
        }

        public ProjectViewModel GetProjectsByProjectID(int ProjectID)
        {
            Project c = cr.GetProjectsByProjectID(ProjectID).FirstOrDefault();
            ProjectViewModel cvm = null;
            if (c != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Project, ProjectViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                cvm = mapper.Map<Project, ProjectViewModel>(c);
            }
            return cvm;
        }

    }
}
