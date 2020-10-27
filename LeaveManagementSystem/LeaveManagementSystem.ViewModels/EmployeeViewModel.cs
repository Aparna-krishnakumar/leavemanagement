using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.ViewModels
{
    public class EmployeeViewModel
    {
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Designation { get; set; }
        [Required]
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Photo { get; set; }
       
    }
}
