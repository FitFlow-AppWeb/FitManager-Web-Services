namespace FitManager_Web_Services.Employees.Domain.Model.Aggregates
{
    public class Specialty
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public Specialty(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description cannot be null or empty.", nameof(description));

            Name = name;
            Description = description;
        }

        protected Specialty() { }
    }
}