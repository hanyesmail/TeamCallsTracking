using System.ComponentModel.DataAnnotations;

namespace TeamCallsTracking.Data.Models;

public class CallStatus
{
    [Key, Required]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string StatusName { get; set; }  = string.Empty;

    public ICollection<Call> Calls { get; set; } = [];
}