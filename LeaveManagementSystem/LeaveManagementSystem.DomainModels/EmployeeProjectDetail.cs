using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.DomainModels
{
    public class EmployeeProjectDetail
    {
        [Key]
        public int EmployeeID { get; set; }
        public int ProjectID { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("ProjectID")]
        public virtual Project Project { get; set; }
        //public virtual List<Project> Projects { get; set; }
    }
}
