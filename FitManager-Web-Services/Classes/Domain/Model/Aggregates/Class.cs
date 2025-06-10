using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Classes.Domain.Model.Aggregates;

public class Class
{
    public int Id { get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public int Capacity { get; set; }
    public DateTime StartDate { get; set; }
    public int Duration { get; set; }
    public string Status { get; set; }
    public int EmployeeId { get; set; }
    
    public Employee Employee { get; set; }
    public ICollection<ClassMember> ClassMembers { get; set; } = new List<ClassMember>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    
    public ICollection<ItemBooking> ItemBookings { get; set; } = new List<ItemBooking>();
    
    public Class(string name, string description, string type, int capacity, DateTime startDate, int duration, string status, int employeeId)
    {
        Name = name;
        Description = description;
        Type = type;
        Capacity = capacity;
        StartDate = startDate;
        Duration = duration;
        Status = status;
        EmployeeId = employeeId;
    }

    protected Class() { }
}