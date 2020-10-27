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
    public interface ILeaveDataService
    {
        void InsertLeaveData(LeaveDataViewModel uvm);
        void ResponseToLeaveData(LeaveDataViewModel uvm);
        void DeleteLeaveData(int uid);
        List<LeaveDataViewModel> GetLeaveData();
        LeaveDataViewModel GetLeaveDataByEmployeeID(int EmployeeID);
    }
    class LeaveDataService:ILeaveDataService
    {
        ILeaveData ur;

        public LeaveDataService()
        {
            ur = new LeaveDataRepository();
        }
        public void InsertLeaveData(LeaveDataViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<LeaveDataViewModel, LeaveData>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            LeaveData u = mapper.Map<LeaveDataViewModel, LeaveData>(uvm);
            ur.InsertLeaveData(u);
        }
        public void ResponseToLeaveData(LeaveDataViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<LeaveDataViewModel, LeaveData>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            LeaveData u = mapper.Map<LeaveDataViewModel, LeaveData>(uvm);
            ur.ResponseToLeaveData(u);
        }
        public void DeleteLeaveData(int uid)
        {
            ur.DeleteLeaveData(uid);
        }
        public List<LeaveDataViewModel> GetLeaveData()
        {
            List<LeaveData> u = ur.GetLeaveData();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<LeaveDataViewModel, LeaveData>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<LeaveDataViewModel> uvm = mapper.Map<List<LeaveData>, List<LeaveDataViewModel>>(u);
            return uvm;
        }
        public LeaveDataViewModel GetLeaveDataByEmployeeID(int EmployeeID)
        {
            LeaveData u = ur.GetLeaveDataByEmployeeID(EmployeeID).FirstOrDefault();
            LeaveDataViewModel uvm = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<LeaveData, LeaveDataViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map < LeaveData, LeaveDataViewModel > (u);
            }
            return uvm;
        }
    }

}
