using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamCallsTracking.Data;
using TeamCallsTracking.Data.Dtos.Call;
using TeamCallsTracking.Data.Models;
using TeamCallsTracking.Data.Models.General;

namespace TeamCallsTracking.Controllers;

[ApiController]
[Route("api/call")]
public class CallController(AppDbContext context) : ControllerBase
{
    [HttpGet("/getAllCalls")]
    public async Task<ActionResult<GenericResponse>> GetAllCalls()
    {
        var calls = await context.Calls
            .Include(c => c.Status)
            .Include(c => c.ClientData)
            .Include(c => c.EmployeeData)
            .Select(c => new CallDto
            {
                Id = c.Id,
                CallDate = c.CallDate,
                CallStatus = c.Status == null ? "" : c.Status.StatusName,
                CallStatusId = c.CallStatusId,
                ClientId = c.ClientId,
                ClientName = c.ClientData == null ? "" : c.ClientData.FirstName + " " + c.ClientData.SecondName,
                EmployeeId = c.EmployeeId,
                EmployeeName = c.EmployeeData == null ? "" : c.EmployeeData.FirstName + " " + c.EmployeeData.SecondName,
                DurationInSeconds =  c.DurationInSeconds,
                Notes = c.Notes,
            }).ToListAsync();

        return Ok(new GenericResponse
        {
            Success = true,
            Message = "All calls have been successfully retrieved.",
            Data = calls
        });
    }

    [HttpGet("/getCallById/{id}")]
    public async Task<ActionResult<CallDto>> GetCallById(int id)
    {
        var call = await context.Calls
            .Include(c => c.Status)
            .Include(c => c.ClientData)
            .Include(c => c.EmployeeData)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (call == null)
        {
            return NotFound(new GenericResponse
            {
                Success = false,
                Message = "Call not found."
            });
        }

        return Ok(new GenericResponse
        {
            Success = true,
            Message = "Call has been successfully retrieved.",
            Data = new CallDto
            {
                Id = call.Id,
                CallDate = call.CallDate,
                CallStatusId = call.CallStatusId,
                CallStatus = call.Status?.StatusName ?? "",
                ClientId = call.ClientId,
                ClientName = call.ClientData == null ? "" : call.ClientData.FirstName + " " + call.ClientData.SecondName,
                EmployeeId = call.EmployeeId,
                EmployeeName = call.EmployeeData == null ? "" : call.EmployeeData.FirstName + " " + call.EmployeeData.SecondName,
                DurationInSeconds = call.DurationInSeconds,
                Notes = call.Notes,
            }
        });
    }

    [HttpPost("/addCall")]
    public async Task<ActionResult<GenericResponse>> AddCall([FromBody] CreateCallDto callDto)
    {
        var employeeIdExists = await context.Employees.AnyAsync(c => c.Id == callDto.EmployeeId);
        if (!employeeIdExists)
        {
            return BadRequest(new GenericResponse
                {
                    Success = false,
                    Message = "Employee not found."
                }
            );
        }
        
        var clientIdExists = await context.Clients.AnyAsync(c => c.Id == callDto.ClientId);
        if (!clientIdExists)
        {
            return BadRequest(new GenericResponse
                {
                    Success = false,
                    Message = "Client id not found."
                }
            );
        }
        
        var callStatusIdExists = await context.CallStatuses.AnyAsync(c => c.Id == callDto.CallStatusId);
        if (!callStatusIdExists)
        {
            return BadRequest(new GenericResponse
                {
                    Success = false,
                    Message = "Call status not found."
                }
            );
        }

        var newCall = new Call
        {
            CallStatusId = callDto.CallStatusId,
            ClientId = callDto.ClientId,
            EmployeeId = callDto.EmployeeId,
            DurationInSeconds = callDto.DurationInSeconds,
            Notes = callDto.Notes,
        };
        
        await context.Calls.AddAsync(newCall);
        await context.SaveChangesAsync();
        
        var addedCall = await context.Calls
            .Include(c => c.Status)
            .Include(c => c.ClientData)
            .Include(c => c.EmployeeData)
            .FirstOrDefaultAsync(c => c.Id == newCall.Id);

        if (addedCall == null)
        {
            return BadRequest(new GenericResponse
            {
                Success = false,
                Message = "Failed to add call, Please try again."
            });
        }
        
        return Ok(new GenericResponse
        {
            Success = true,
            Message = "Call has been successfully added.",
            Data = new CallDto
            {
                Id = addedCall.Id,
                CallDate = DateTime.Now,
                CallStatusId = addedCall.CallStatusId,
                CallStatus =  addedCall.Status?.StatusName ?? "",
                ClientId = addedCall.ClientId,
                ClientName = (addedCall.ClientData?.FirstName ?? "") + " " + (addedCall.ClientData?.SecondName ?? ""),
                EmployeeId = addedCall.EmployeeId,
                EmployeeName = (addedCall.EmployeeData?.FirstName ?? "") + " " + (addedCall.EmployeeData?.SecondName ?? ""),
                DurationInSeconds = addedCall.DurationInSeconds,
                Notes = addedCall.Notes ?? "",
            }
        });
    }

    [HttpPut("/updateCall")]
    public async Task<ActionResult<GenericResponse>> UpdateCall([FromBody] UpdateCallDto updateCallDto)
    {
        
        var updatedCall = await context.Calls
            .FirstOrDefaultAsync(c => c.Id == updateCallDto.Id);

        if (updatedCall == null)
        {
            return NotFound(new GenericResponse
            {
                Success = false,
                Message = "Call not found."
            });
        }
        
        
        
        if(updateCallDto.CallStatusId != null)
        {
            updatedCall.CallStatusId = updateCallDto.CallStatusId ?? 0;
        }
        
        updatedCall.Notes = updateCallDto.Notes;
        
        await context.SaveChangesAsync();

        return Ok(new GenericResponse
            {
                Success = true,
                Message = "Call has been successfully updated",
            }
        );
    }

    [HttpDelete("/deleteCall/{id}")]
    public async Task<ActionResult<GenericResponse>> DeleteCall(int id)
    {
        var deletedCall = await context.Calls.FirstOrDefaultAsync(c => c.Id == id);

        if (deletedCall == null)
        {
            return BadRequest(new GenericResponse
            {
                Success = false,
                Message = "Call not found."
            });
        }

        context.Calls.Remove(deletedCall);
        await context.SaveChangesAsync();

        return Ok(new GenericResponse
            {
                Success = true,
                Message = "Call has been successfully deleted",
            }
        );
    }
}