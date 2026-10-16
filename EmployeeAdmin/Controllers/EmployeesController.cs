using EmployeeAdmin.Data;
using EmployeeAdmin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext;
        public EmployeesController(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _dbcontext.Employees.ToList();
            return Ok(employees);
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetEmployee(Guid id)
        {
            var employee = _dbcontext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployee(AddemployeeDTO addemployeeDTO)
        {
            var newEmployee = new Employee
            {
                Names = addemployeeDTO.Names,
                Email = addemployeeDTO.Email,
                Phone = addemployeeDTO.Phone,
                Salary = addemployeeDTO.Salary
            };

            _dbcontext.Employees.Add(newEmployee);
            _dbcontext.SaveChanges();

            return Ok(newEmployee);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id,UpdateEmployeeDto updateEmployeeDto)
        {
            var existingEmployee = _dbcontext.Employees.Find(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.Names=updateEmployeeDto.Names;
            existingEmployee.Email=updateEmployeeDto.Email;
            existingEmployee.Phone=updateEmployeeDto.Phone;
            existingEmployee.Salary=updateEmployeeDto.Salary;

            _dbcontext.SaveChanges();

            return Ok(existingEmployee);
        }
    }
}
