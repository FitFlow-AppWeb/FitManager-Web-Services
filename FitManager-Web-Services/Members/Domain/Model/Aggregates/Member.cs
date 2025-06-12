using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using System.Collections.Generic;
using System;

namespace FitManager_Web_Services.Members.Domain.Model.Aggregates
{
    public class Member
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Age { get; private set; }
        public int Dni { get; private set; }
        public int PhoneNumber { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }

        public ICollection<ClassMember> ClassMembers { get; private set; } = new List<ClassMember>();
        public ICollection<Booking> Bookings { get; private set; } = new List<Booking>();
        public ICollection<Attendance> Attendances { get; private set; } = new List<Attendance>();
        
        public MembershipStatus MembershipStatus { get; private set; }
        

        public Member(string firstName, string lastName, int age, int dni, int phoneNumber, string address, string email)
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

            ClassMembers = new List<ClassMember>();
            Bookings = new List<Booking>();
            Attendances = new List<Attendance>();
        }

        protected Member() { }

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
        
        public void UpdateMembershipStatus(DateTime? startDate, DateTime? endDate, string? status, int? membershipTypeId)
        {
            if (startDate.HasValue)
            {
                MembershipStatus.StartDate = startDate.Value;
            }
            if (endDate.HasValue)
            {
                MembershipStatus.EndDate = endDate.Value;
            }
            if (!string.IsNullOrEmpty(status)) 
            {
                MembershipStatus.Status = status;
            }
            if (membershipTypeId.HasValue)
            {
                MembershipStatus.MembershipTypeId = membershipTypeId.Value;
            }
        }

        public void AssignMembershipStatus(MembershipStatus status)
        {
            if (status == null) throw new ArgumentNullException(nameof(status), "Membership status cannot be null.");
            MembershipStatus = status;
        }
        
        public void SetMembershipStatus(MembershipStatus status)
        {
            this.MembershipStatus = status;
        }
        
    }
}