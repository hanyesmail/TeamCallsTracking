using System.ComponentModel.DataAnnotations;

namespace TeamCallsTracking.Data.Dtos.EmployeeTitle;

public class CreateEmployeeTitleDto
{
    [Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;
}