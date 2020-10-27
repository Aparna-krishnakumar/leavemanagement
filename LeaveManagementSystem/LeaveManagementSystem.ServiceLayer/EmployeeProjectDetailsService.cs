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
    public interface IEmployeeProjectDetailsService
    {
        void InsertEmployeeProject(EditEmployeeProjectDetailsViewModel qvm);

        EditEmployeeProjectDetailsViewModel GetEmployeeProjectsByEmployeeID(int EmployeeID);
        void DeleteEmployeeProject(int epid, int pid);
        EditEmployeeProjectDetailsViewModel GetEmployeeProjectsByProjectID(int ProjectID);

    }
    public class EmployeeProjectDetailsService : IEmployeeProjectDetailsService
    {
        IEmployeeProjects qr;

        public EmployeeProjectDetailsService()
        {
            qr = new EmployeeProjectsRepository();
        }
        public void InsertEmployeeProject(EditEmployeeProjectDetailsViewModel qvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditEmployeeProjectDetailsViewModel, EmployeeProjectDetail>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            EmployeeProjectDetail q = mapper.Map<EditEmployeeProjectDetailsViewModel, EmployeeProjectDetail>(qvm);
            qr.InsertEmployeeProject(q);
        }
        public EditEmployeeProjectDetailsViewModel GetEmployeeProjectsByEmployeeID(int EmployeeID)
        {
            EmployeeProjectDetail u = qr.GetEmployeeProjectsByEmployeeID(EmployeeID).FirstOrDefault();
            EditEmployeeProjectDetailsViewModel uvm = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<EmployeeProjectDetail, EditEmployeeProjectDetailsViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<EmployeeProjectDetail, EditEmployeeProjectDetailsViewModel>(u);
            }
            return uvm;
        }
        public void DeleteEmployeeProject(int epid, int pid)
        {
            qr.DeleteEmployeeProject(epid, pid);
        }
        public EditEmployeeProjectDetailsViewModel GetEmployeeProjectsByProjectID(int ProjectID)
        {
            EmployeeProjectDetail u = qr.GetEmployeeProjectsByEmployeeID(ProjectID).FirstOrDefault();
            EditEmployeeProjectDetailsViewModel uvm = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<EmployeeProjectDetail, EditEmployeeProjectDetailsViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<EmployeeProjectDetail, EditEmployeeProjectDetailsViewModel>(u);
            }
            return uvm;
        }
    }
}
