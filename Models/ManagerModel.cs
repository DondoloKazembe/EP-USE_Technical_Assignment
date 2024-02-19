using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EPI_USE_Web_Application.Models
{
    public class ManagerModel
    {
        // The following are properties that will set and retrieve data from database 

        [Key]
        [Required] // This ensures that the information must be provided 
        public string ManagerNumber { get; set; }

        [Required] // This ensures that the information must be provided 
        public string ManagerName { get; set; }

        [Required] // This ensures that the information must be provided 
        public string ManagerSurname { get; set; }

        [Required] // This ensures that the information must be provided 
        public int ManagerSalary { get; set; }
    }
}
