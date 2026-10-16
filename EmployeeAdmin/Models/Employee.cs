using System;


namespace EmployeeAdmin.Models
{
    public class Employee
    {
         public Guid Id { get; set; }
         public required string Names { get; set; }
         public required string? Email { get; set; }
         public string? Phone { get; set; }
         public decimal Salary { get; set; }
    
    }
}
