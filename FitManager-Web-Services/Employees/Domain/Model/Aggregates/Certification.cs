using FitManager_Web_Services.Employees.Application;

namespace FitManager_Web_Services.Employees.Domain.Model.Aggregates
{
    public class Certification
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int EmployeeId { get; private set; }
        public Employee Employee { get; private set; }

        public Certification(string name, string description, int employeeId)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description cannot be null or empty.", nameof(description));
            if (employeeId <= 0) throw new ArgumentOutOfRangeException(nameof(employeeId), "EmployeeId must be a positive number.");

            Name = name;
            Description = description;
            EmployeeId = employeeId;
        }

        protected Certification() { }
    }
}