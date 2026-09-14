using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCallsTracking.Data.Dtos.Call;

public class CreateCallDto
{
    public int EmployeeId { get; set; }
    
    [Required] 
    public int ClientId { get; set; }
    
    public DateTime CallDate { get; set; } =  DateTime.UtcNow;
    
    [Range(0, int.MaxValue)]
    public int DurationInSeconds { get; set; }

    [Required]
    public int CallStatusId { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }
}