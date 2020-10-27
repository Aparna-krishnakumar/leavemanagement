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
    public interface IEmployeesService
    {
        int InsertEmployee(EmployeeViewModel uvm);
        void UpdateEmployeeDetails(EditEmployeeDetailsViewModel uvm);
        void UpdateEmployeePassword(EditEmployeePasswordViewModel uvm);
        void DeleteEmployee(int uid);
        List<EmployeeViewModel> GetEmployees();
        EmployeeViewModel GetEmployeesByEmailAndPassword(string Email, string Password);
        EmployeeViewModel GetEmployeesByEmail(string Email);
        EmployeeViewModel GetEmployeesByEmployeeID(int EmployeeID);
    }

    public class EmployeesService : IEmployeesService
    {
        IEmployees ur;

        public EmployeesService()
        {
            ur = new EmployeesRepository();
        }
        public int InsertEmployee(EmployeeViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EmployeeViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee u = mapper.Map<EmployeeViewModel, Employee>(uvm);
            u.PasswordHash = SHA256HashGenerator.GenerateHash(uvm.Password);
            ur.InsertEmployee(u);
            int uid = ur.GetLatestEmployeeID();
            return uid;
        }
        public void UpdateEmployeeDetails(EditEmployeeDetailsViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditEmployeeDetailsViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee u = mapper.Map<EditEmployeeDetailsViewModel, Employee>(uvm);
            ur.UpdateEmployeeDetails(u);
        }

        public void UpdateEmployeePassword(EditEmployeePasswordViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditEmployeePasswordViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee u = mapper.Map<EditEmployeePasswordViewModel, Employee>(uvm);
            u.PasswordHash = SHA256HashGenerator.GenerateHash(uvm.Password);
            ur.UpdateEmployeePassword(u);
        }

        public void DeleteEmployee(int uid)
        {
            ur.DeleteEmployee(uid);
        }

        public List<EmployeeViewModel> GetEmployees()
        {
            List<Employee> u = ur.GetEmployees();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<EmployeeViewModel> uvm = mapper.Map<List<Employee>, List<EmployeeViewModel>>(u);
            return uvm;
        }

        public EmployeeViewModel GetEmployeesByEmailAndPassword(string Email, string Password)
        {
            Employee u = ur.GetEmployeesByEmailAndPassword(Email, SHA256HashGenerator.GenerateHash(Password)).FirstOrDefault();
            EmployeeViewModel uvm = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<Employee, EmployeeViewModel>(u);
            }
            return uvm;
        }

        public EmployeeViewModel GetEmployeesByEmail(string Email)
        {
            Employee u = ur.GetEmployeesByEmail(Email).FirstOrDefault();
            EmployeeViewModel uvm = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<Employee, EmployeeViewModel>(u);
            }
            return uvm;
        }

        public EmployeeViewModel GetEmployeesByEmployeeID(int EmployeeID)
        {
            Employee u = ur.GetEmployeesByEmployeeID(EmployeeID).FirstOrDefault();
            EmployeeViewModel uvm = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<Employee, EmployeeViewModel>(u);
            }
            return uvm;
        }

    }
}
