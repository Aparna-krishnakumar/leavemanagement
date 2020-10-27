using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.ViewModels
{
    public class ProjectViewModel
    {
        [Required]
        public int ProjectID { get; set; }

        [Required]
        public string ProjectName { get; set; }
        [Required]
        public int ProjectManager { get; set; }
    
}
}
