using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module07DataAccess.Model
{
    public class Employee
    {
        public int EmployeeID { get; set; } // Corresponds to EmployeeID in the table
        public string Name { get; set; }     // Corresponds to Name in the table
        public string Address { get; set; }  // Corresponds to Address in the table
        public string Email { get; set; }    // Corresponds to Email in the table
        public string ContactNo { get; set; } // Corresponds to ContactNo in the table
    }
}
