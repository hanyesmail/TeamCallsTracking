namespace TeamCallsTracking.Data.Models.General;

public class GeneralResponse
{
    public bool Success { get; set; } = true;

    public string Message { get; set; } =  string.Empty;

    public Object? Data { get; set; } = null;
}