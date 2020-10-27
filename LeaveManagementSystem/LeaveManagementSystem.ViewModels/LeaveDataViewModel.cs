using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.ViewModels
{
    public class LeaveDataViewModel
    {
        [Required]
        public int LeaveID { get; set; }
        
        public int EmployeeID { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string ReasonOfAbsence { get; set; }
        public bool Approved { get; set; }
    }
}
