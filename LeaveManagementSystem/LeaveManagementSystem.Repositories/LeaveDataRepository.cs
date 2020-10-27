using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagementSystem.DomainModels;

namespace LeaveManagementSystem.Repositories
{
    public interface ILeaveData
    {
        void InsertLeaveData(LeaveData e);
        void ResponseToLeaveData(LeaveData e);
        void DeleteLeaveData(int eid);
        List<LeaveData> GetLeaveData();
        List<LeaveData> GetLeaveDataByEmployeeID(int EmployeeID);
    }
    public class LeaveDataRepository : ILeaveData
    {
        LeaveManagementSystemDatabaseDbContext db;
        public LeaveDataRepository()
        {
            db = new LeaveManagementSystemDatabaseDbContext();
        }
        public void InsertLeaveData(LeaveData e)
        {

            db.LeaveDatas.Add(e);
            db.SaveChanges();
        }
        public void ResponseToLeaveData(LeaveData e)
        {
            LeaveData us = db.LeaveDatas.Where(temp => temp.LeaveID == e.LeaveID).FirstOrDefault();
            if (us != null)
            {
                us.Approved = e.Approved;
                db.SaveChanges();
            }
        }
        public void DeleteLeaveData(int eid)
        {
            LeaveData us = db.LeaveDatas.Where(temp => temp.LeaveID == eid).FirstOrDefault();
            if (us != null)
            {
                db.LeaveDatas.Remove(us);
                db.SaveChanges();
            }
        }
        public List<LeaveData> GetLeaveData()
        {
            List<LeaveData> us = db.LeaveDatas.ToList();
            return us;
        }
        public List<LeaveData> GetLeaveDataByEmployeeID(int EmployeeID)
        {
            List<LeaveData> us = db.LeaveDatas.Where(temp => temp.EmployeeID == EmployeeID).ToList();
            return us;
        }
    }
}
