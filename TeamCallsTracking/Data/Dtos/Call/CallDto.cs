namespace TeamCallsTracking.Data.Dtos.Call;

public class CallDto
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = string.Empty;
    
    public int ClientId { get; set; }
    
    public string ClientName { get; set; } = string.Empty;
    
    public DateTime CallDate { get; set; }

    public int CallStatusId { get; set; }
    
    public string CallStatus { get; set; } = string.Empty;
    
    public string? Notes { get; set; }
}