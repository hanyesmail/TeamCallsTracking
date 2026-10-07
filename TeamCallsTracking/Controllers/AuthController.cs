using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TeamCallsTracking.Data;
using TeamCallsTracking.Data.Dtos.Auth;
using TeamCallsTracking.Data.Dtos.Employee;
using TeamCallsTracking.Data.Models;
using TeamCallsTracking.Data.Models.General;
using TeamCallsTracking.Settings;

namespace TeamCallsTracking.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(AppDbContext context, IConfiguration configuration) : ControllerBase
{
    [HttpPost("/register")]
    public async Task<ActionResult<GenericResponse>> AddEmployee([FromBody] CreateEmployeeDto employee)
    {
        var emailExists = await context.Employees.AnyAsync(x => x.Email == employee.Email);
        if (emailExists)
        {
            return BadRequest(new GenericResponse
            {
                Success = false,
                Message = "Email already exists"
            });
        }
        
        var newEmployee = new Employee
        {
            FirstName = employee.FirstName,
            SecondName = employee.LastName,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            EmployeeTitleId = employee.EmployeeTitleId,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(employee.Password),
            Role = employee.Role,
            IsActive = employee.IsActive
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
            Message = "Employee registered successfully.",
            Data = new EmployeeDto
            {
                Id = addedEmployee.Id,
                FirstName = addedEmployee.FirstName,
                LastName = addedEmployee.SecondName,
                Email = addedEmployee.Email,
                Role = addedEmployee.Role,
                IsActive = addedEmployee.IsActive,
                PhoneNumber = addedEmployee.PhoneNumber,
                EmployeeTitleId = addedEmployee.EmployeeTitleId,
                EmployeeTitleName = addedEmployee.EmployeeTitle != null ?
                    addedEmployee.EmployeeTitle.TitleName : string.Empty
            }
        });
    }

    [HttpPost("/login")]
    public async Task<ActionResult<GenericResponse>> Login([FromBody] LoginRequestDto loginRequest)
    {
        var employee = await context.Employees.FirstOrDefaultAsync(e => e.Email == loginRequest.Email);

        if (employee == null)
        {
            return Unauthorized(new GenericResponse
            {
                Success = false,
                Message = "Invalid email or password!"
            });
        }

        if (!employee.IsActive)
        {
            return Unauthorized(new GenericResponse
            {
                Success = false,
                Message = "This account is not active!"
            });
        }

        var isValidPassword = BCrypt.Net.BCrypt.Verify(
            loginRequest.Password, employee.PasswordHash);
        if (!isValidPassword)
        {
            return Unauthorized(new GenericResponse
            {
                Success = false,
                Message = "Invalid password!"
            });
        }
        
        var loginResponse = GenerateJwtToken(employee);
        return Ok(new GenericResponse
            {
                Success = true,
                Message = "Successfully logged in!",
                Data = loginResponse
            }
        );
    }
    
    private LoginResponseDto GenerateJwtToken(
        Employee employee)
    {
        var jwtSettings = configuration
            .GetSection("Jwt")
            .Get<JwtSettings>()!;

        var expiresAt = DateTime.UtcNow.AddDays(jwtSettings.ExpirationDays);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, employee.Id.ToString()),

            new(ClaimTypes.Name, $"{employee.FirstName} {employee.SecondName}"),

            new(ClaimTypes.Email, employee.Email),

            new(ClaimTypes.Role, employee.Role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings.Key));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler()
            .WriteToken(tokenDescriptor);

        return new LoginResponseDto
        {
            Token = token,
            ExpiresAt = expiresAt
        };
    }

}