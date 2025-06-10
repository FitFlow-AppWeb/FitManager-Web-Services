using FitManager_Web_Services.Classes.Domain.Model.Aggregates; // Asegúrate de importar esto

namespace FitManager_Web_Services.Employees.Domain.Model.Aggregates
{
    public class Employee
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int Dni { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public float Wage { get; set; }
        public string Role { get; set; }
        public int WorkHours { get; set; }

        public ICollection<Class> Classes { get; set; } = new List<Class>();
        public ICollection<Specialty> Specialties { get; set; } = new List<Specialty>();
        public ICollection<Certification> Certifications { get; set; } = new List<Certification>();

        public Employee(string firstName, string lastName, int age, int dni, int phoneNumber, string address, string email, string password, float wage, string role, int workHours)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Dni = dni;
            PhoneNumber = phoneNumber;
            Address = address;
            Email = email;
            Password = password;
            Wage = wage;
            Role = role;
            WorkHours = workHours;
        }

        protected Employee() { }
    }
}