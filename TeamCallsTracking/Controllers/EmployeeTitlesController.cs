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
    [HttpGet("/getAll")]
    public async Task<ActionResult<GeneralResponse>> GetAll()
    {
        var titles = await context.EmployeeTitles
            .Select(et => new EmployeeTitleDto 
            {
                Id = et.Id,
                TitleName = et.TitleName 
            }).ToListAsync();

        return Ok(new GeneralResponse
        {
            Success = true,
            Message = "Data successfully retrieved!",
            Data = titles
        });
    }


    [HttpGet("/byId/{id}")]
    public async Task<ActionResult<GeneralResponse>> GetTitleById(int id)
    {
        var title = await context.EmployeeTitles
            .FirstOrDefaultAsync(et => et.Id == id);

        if (title == null)
        {
            return NotFound(new GeneralResponse
            {
                Success = false,
                Message = "The specified title does not exist!"
            });
        }

        return Ok(new GeneralResponse
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

    [HttpPost("/create")]
    public async Task<ActionResult<GeneralResponse>> Create([FromBody] CreateEmployeeTitleDto title)
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
            return BadRequest(new GeneralResponse
            {
                Success = false,
                Message = "Failed to create the title!, Please try again!",
            });
        }

        return Ok(new GeneralResponse
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

    [HttpPut("/update")]
    public async Task<ActionResult<GeneralResponse>> UpdateEmployeeTitle([FromBody] EmployeeTitleDto title)
    {
        var updatedTitle = await context.EmployeeTitles
            .FirstOrDefaultAsync(et => et.Id == title.Id);

        if (updatedTitle == null)
        {
            return NotFound(new GeneralResponse
            {
                Success = false,
                Message = "This employee title does not exist!"
            });
        }
        
        updatedTitle.TitleName = title.TitleName;
        await context.SaveChangesAsync();
        return Ok(new GeneralResponse
            {
                Success = true,
                Message = "Employee title updated successfully!",
            }
        );
    }

    [HttpDelete("/delete/{id}")]
    public async Task<ActionResult<GeneralResponse>> DeleteEmployeeTitle(int id)
    {
        var deletedTitle = await context.EmployeeTitles.FirstOrDefaultAsync(et => et.Id == id);
        
        if (deletedTitle  == null)
        {
            return NotFound(new GeneralResponse
            {
                Success = false,
                Message = "This employee title does not exist!"
            });
        }

        context.EmployeeTitles.Remove(deletedTitle);
        await context.SaveChangesAsync();
        return Ok(new GeneralResponse
            {
                Success = true,
                Message = "Employee title deleted successfully!",
            }
        );
    }
}