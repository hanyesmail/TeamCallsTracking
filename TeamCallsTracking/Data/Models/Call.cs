using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCallsTracking.Data.Models;

public class Call
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
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

    [ForeignKey(nameof(CallStatusId))]
    public CallStatus? Status { get; set; } = null;

    [ForeignKey(nameof(ClientId))] 
    public Client? ClientData { get; set; } = null;

    [ForeignKey(nameof(EmployeeId))]
    public Employee? EmployeeData { get; set; } = null;
}