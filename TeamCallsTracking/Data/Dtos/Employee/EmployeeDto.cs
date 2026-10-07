namespace TeamCallsTracking.Data.Dtos.Employee;

public class EmployeeDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public int EmployeeTitleId { get; set; }

    public string? EmployeeTitleName { get; set; }
}