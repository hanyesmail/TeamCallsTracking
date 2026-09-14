using System.ComponentModel.DataAnnotations;

namespace TeamCallsTracking.Data.Dtos.Employee;

public class CreateEmployeeDto
{
    [Required, MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required, MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    
    [Required, EmailAddress, MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    
    [Required, MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public int EmployeeTitleId { get; set; }
}