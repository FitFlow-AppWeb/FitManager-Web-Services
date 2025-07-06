// Classes/Interfaces/REST/Resources/RawAttendanceResource.cs

namespace FitManager_Web_Services.Classes.Interfaces.REST.Resources;

public class RawAttendanceResource
{
    public int Id { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime? ExitTime { get; set; } 
    public int MemberId { get; set; }
    public int ClassId { get; set; }
}