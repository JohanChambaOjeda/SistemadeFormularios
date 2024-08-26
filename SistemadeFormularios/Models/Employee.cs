using System;

namespace SistemadeFormularios.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int JobID { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
    }
}
