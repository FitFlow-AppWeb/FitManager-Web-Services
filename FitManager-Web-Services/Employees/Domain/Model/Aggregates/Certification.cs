namespace FitManager_Web_Services.Employees.Domain.Model.Aggregates
{
    public class Certification
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public Certification(string name, string description, int employeeId)
        {
            Name = name;
            Description = description;
            EmployeeId = employeeId;
        }

        protected Certification() { }
    }
}