using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LeaveManagementSystem.ViewModels
{
    public class EditEmployeeProjectDetailsViewModel
    {
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public int ProjectID { get; set; }
    }
}
