using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamCallsTracking.Data;
using TeamCallsTracking.Data.Dtos.EmployeeTitle;
using TeamCallsTracking.Data.Models;
using TeamCallsTracking.Data.Models.General;

namespace TeamCallsTracking.Controllers;

[ApiController]
[Route("api/employeeTitles")]
public class EmployeeTitlesController(AppDbContext context) : ControllerBase
{
    [HttpGet("/getAllEmployeeTitles")]
    public async Task<ActionResult<GenericResponse>> GetAll()
    {
        var titles = await context.EmployeeTitles
            .Select(et => new EmployeeTitleDto 
            {
                Id = et.Id,
                TitleName = et.TitleName 
            }).ToListAsync();

        return Ok(new GenericResponse
        {
            Success = true,
            Message = "Data successfully retrieved!",
            Data = titles
        });
    }


    [HttpGet("/getEmployeeTitleById/{id}")]
    public async Task<ActionResult<GenericResponse>> GetTitleById(int id)
    {
        var title = await context.EmployeeTitles
            .FirstOrDefaultAsync(et => et.Id == id);

        if (title == null)
        {
            return NotFound(new GenericResponse
            {
                Success = false,
                Message = "The specified title does not exist!"
            });
        }

        return Ok(new GenericResponse
        {
            Success = true,
            Message = "Data successfully retrieved!",
            Data = new EmployeeTitleDto
            {
                Id = title.Id,
                TitleName = title.TitleName
            }
        });
    }

    [HttpPost("/addEmployeeTitle")]
    public async Task<ActionResult<GenericResponse>> AddEmployeeTitle([FromBody] CreateEmployeeTitleDto title)
    {
        var employeeTitle = new EmployeeTitle
        {
            TitleName = title.Name
        };

        await context.EmployeeTitles.AddAsync(employeeTitle);
        await context.SaveChangesAsync();
        
        var createdTitle = await context.EmployeeTitles.FirstOrDefaultAsync(et => et.Id == employeeTitle.Id);
        if (createdTitle == null)
        {
            return BadRequest(new GenericResponse
            {
                Success = false,
                Message = "Failed to create the title!, Please try again!",
            });
        }

        return Ok(new GenericResponse
        {
            Success = true,
            Message = "Employee title created successfully!",
            Data = new EmployeeTitleDto
            {
                Id = createdTitle.Id,
                TitleName = createdTitle.TitleName
            }
        });
    }

    [HttpPut("/updateEmployeeTitle")]
    public async Task<ActionResult<GenericResponse>> UpdateEmployeeTitle([FromBody] EmployeeTitleDto title)
    {
        var updatedTitle = await context.EmployeeTitles
            .FirstOrDefaultAsync(et => et.Id == title.Id);

        if (updatedTitle == null)
        {
            return NotFound(new GenericResponse
            {
                Success = false,
                Message = "This employee title does not exist!"
            });
        }
        
        updatedTitle.TitleName = title.TitleName;
        await context.SaveChangesAsync();
        return Ok(new GenericResponse
            {
                Success = true,
                Message = "Employee title updated successfully!",
            }
        );
    }

    [HttpDelete("/deleteEmployeeTitle/{id}")]
    public async Task<ActionResult<GenericResponse>> DeleteEmployeeTitle(int id)
    {
        var deletedTitle = await context.EmployeeTitles.FirstOrDefaultAsync(et => et.Id == id);
        
        if (deletedTitle  == null)
        {
            return NotFound(new GenericResponse
            {
                Success = false,
                Message = "This employee title does not exist!"
            });
        }

        context.EmployeeTitles.Remove(deletedTitle);
        await context.SaveChangesAsync();
        return Ok(new GenericResponse
            {
                Success = true,
                Message = "Employee title deleted successfully!",
            }
        );
    }
}