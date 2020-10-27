using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace LeaveManagementSystem.DomainModels
{
    public class LeaveData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReasonOfAbsence { get; set; }
        [DefaultValue(false)]
        public bool Approved { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }


    }
}
