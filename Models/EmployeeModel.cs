using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EPI_USE_Web_Application.Models
{
    public class EmployeeModel
    {
        // The following are properties that will set and retrieve data from database 
        // The required in square brackets ensures that the information must be provided 

        [Key]
        [Required]
        public string EmployeeNumber { get; set; }
        [Required] 
        public string EmployeeName { get; set; }
        [Required] 
        public string EmployeeSurname { get; set; }
        [Required] 
        public DateTime BirthDate { get; set; }
        
        public string ManagerNumber { get; set; }

        [Required]
        public int EmployeeSalary { get; set; }
        [Required]
        public string EmployeePosition { get; set; }

    }
}
