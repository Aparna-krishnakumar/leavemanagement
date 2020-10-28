using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LeaveManagementSystem.DomainModels
{
   public class HRtable
    {
        [Key]
        public int HRID { get; set; }
        public int ProjectID { get; set; }
        [ForeignKey("HRID")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("ProjectID")]
        public virtual Project Project { get; set; }
    }
}
