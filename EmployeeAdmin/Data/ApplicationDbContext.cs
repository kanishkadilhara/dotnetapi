using System;
using EmployeeAdmin.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdmin.Data
{
    public class ApplicationDbContext : DbContext
    {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } 
    }
}
