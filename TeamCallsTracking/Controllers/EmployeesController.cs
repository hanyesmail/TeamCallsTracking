using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamCallsTracking.Data;
using TeamCallsTracking.Data.Dtos.Employee;
using TeamCallsTracking.Data.Models;
using TeamCallsTracking.Data.Models.General;

namespace TeamCallsTracking.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController(AppDbContext context) : ControllerBase
{
    [HttpGet("/getAllEmployees")]
    public async Task<ActionResult<GenericResponse>> GetEmployees()
    {
        var employees = await context.Employees
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.SecondName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                EmployeeTitleId = e.EmployeeTitleId,
                EmployeeTitleName = e.EmployeeTitle != null ? 
                    e.EmployeeTitle.TitleName : string.Empty 
            }).ToListAsync();

        return Ok(new GenericResponse
        {
            Success = true,
            Message = "Employees successfully retrieved",
            Data = employees
        });
    }

    [HttpGet("/getEmployeeById/{id}")]
    public async Task<ActionResult<GenericResponse>> GetEmployeeById(int id)
    {
        var employee = await context.Employees.
            FirstOrDefaultAsync(e => e.Id  == id);

        if (employee == null)
        {
            return NotFound(new GenericResponse
            {
                Success = false,
                Message =  "This employee does not exist!"
            });
        }

        return Ok(new GenericResponse
            {
                Success = true,
                Message = "Employee successfully retrieved",
                Data = employee
            }
        );
    }

    [HttpPost("/addEmployee")]
    public async Task<ActionResult<GenericResponse>> AddEmployee([FromBody] CreateEmployeeDto employee)
    {
        var newEmployee = new Employee
        {
            FirstName = employee.FirstName,
            SecondName = employee.LastName,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            EmployeeTitleId = employee.EmployeeTitleId,
        };
        
        await context.Employees.AddAsync(newEmployee);
        await context.SaveChangesAsync();
        
        var addedEmployee = await context.Employees.
            Include(e => e.EmployeeTitle)
            .FirstOrDefaultAsync(e => e.Id == newEmployee.Id);

        if (addedEmployee == null)
        {
            return BadRequest(new GenericResponse
            {
                Success = false,
                Message = "Failed to add the employee!, Please try again!"
            });
        }

        return Ok(new GenericResponse
        {
            Success = true,
            Message = "Employee successfully added",
            Data = new EmployeeDto
            {
                Id = addedEmployee.Id,
                FirstName = addedEmployee.FirstName,
                LastName = addedEmployee.SecondName,
                Email = addedEmployee.Email,
                PhoneNumber = addedEmployee.PhoneNumber,
                EmployeeTitleId = addedEmployee.EmployeeTitleId,
                EmployeeTitleName = addedEmployee.EmployeeTitle != null ?
                    addedEmployee.EmployeeTitle.TitleName : string.Empty
            }
        });
    }

    [HttpPut("/updateEmployee")]
    public async Task<ActionResult<GenericResponse>> UpdateEmployee(EmployeeDto employee)
    {
        var updatedEmployee = await context.Employees.
            FirstOrDefaultAsync(e => e.Id == employee.Id);

        if (updatedEmployee == null)
        {
            return NotFound(new GenericResponse
            {
                Success = false,
                Message = "This employee does not exist!"
            });
        }

        updatedEmployee.FirstName = employee.FirstName;
        updatedEmployee.SecondName = employee.LastName;
        updatedEmployee.Email = employee.Email;
        updatedEmployee.PhoneNumber = employee.PhoneNumber;
        updatedEmployee.EmployeeTitleId = employee.EmployeeTitleId;

        await context.SaveChangesAsync();
        return Ok(new GenericResponse
            {
                Success = true,
                Message = "Employee successfully updated",
            }
        );
    }

    [HttpDelete(("/deleteEmployee/{id}"))]
    public async Task<ActionResult<GenericResponse>> DeleteEmployee(int id)
    {
        var employee = await context.Employees.
            FirstOrDefaultAsync(e => e.Id  == id);

        if (employee == null)
        {
            return NotFound(new GenericResponse
            {
                Success = false,
                Message = "This employee does not exist!"
            });
        }

        context.Employees.Remove(employee);
        await context.SaveChangesAsync();

        return Ok(new GenericResponse
            {
                Success = true,
                Message = "Employee successfully deleted",
            }
        );
    }
}