using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCallsTracking.Data.Models;

public class Client
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required, MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string SecondName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Email { get; set; }  = string.Empty;
    
    [Required, MaxLength(100)]
    public string PhoneNumber { get; set; }  = string.Empty;
    
    public ICollection<Call> Calls { get; set; } = [];
}