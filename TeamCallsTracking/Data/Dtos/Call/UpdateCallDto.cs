using System.ComponentModel.DataAnnotations;

namespace TeamCallsTracking.Data.Dtos.Call;

public class UpdateCallDto
{
    [Required] 
    public int Id { get; set; }
    
    public int? CallStatusId { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }
}