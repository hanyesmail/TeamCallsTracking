using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCallsTracking.Data.Models;

public class EmployeeTitle
{
    [Key]
    public int Id { get; set; }
    
    [Required, MaxLength(50)]
    public string TitleName { get; set; }  = string.Empty;

    public ICollection<Employee> Employees { get; set; } = [];
}