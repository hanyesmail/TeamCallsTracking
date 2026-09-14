using System.ComponentModel.DataAnnotations;

namespace TeamCallsTracking.Data.Dtos.CallStatus;

public class CreateCallStatusDto
{
    [Required]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string StatusName { get; set; }  = string.Empty;
}