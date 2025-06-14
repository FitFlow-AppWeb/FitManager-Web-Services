using System;
using System.Collections.Generic;

namespace FitManager_Web_Services.Employees.Domain.Model.Aggregates
{
    public class Employee
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Age { get; private set; }
        public int Dni { get; private set; }
        public int PhoneNumber { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }

        // Relaciones de uno a muchos
        public ICollection<Certification> Certifications { get; private set; } = new List<Certification>();
        public ICollection<Specialty> Specialties { get; private set; } = new List<Specialty>();

        public Employee(string firstName, string lastName, int age, int dni, int phoneNumber, string address, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));
            if (age <= 0) throw new ArgumentOutOfRangeException(nameof(age), "Age must be a positive number.");
            if (dni <= 0) throw new ArgumentOutOfRangeException(nameof(dni), "DNI must be a positive number.");
            if (phoneNumber <= 0) throw new ArgumentOutOfRangeException(nameof(phoneNumber), "Phone number must be a positive number.");
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Address cannot be null or empty.", nameof(address));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Dni = dni;
            PhoneNumber = phoneNumber;
            Address = address;
            Email = email;

            Certifications = new List<Certification>();
            Specialties = new List<Specialty>();
        }

        protected Employee() { }

        public void Update(string firstName, string lastName, int age, int dni, int phoneNumber, string address, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name cannot be null or empty during update.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name cannot be null or empty during update.", nameof(lastName));
            if (age < 0) throw new ArgumentOutOfRangeException(nameof(age), "Age cannot be negative.");
            if (dni <= 0) throw new ArgumentOutOfRangeException(nameof(dni), "DNI must be a positive number during update.");
            if (phoneNumber < 0) throw new ArgumentOutOfRangeException(nameof(phoneNumber), "Phone number cannot be negative.");
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Address cannot be null or empty during update.", nameof(address));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be null or empty during update.", nameof(email));

            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Dni = dni;
            PhoneNumber = phoneNumber;
            Address = address;
            Email = email;
        }
    }
}
