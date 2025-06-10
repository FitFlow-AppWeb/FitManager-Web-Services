using FitManager_Web_Services.Classes.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Members.Domain.Model.Aggregates
{
    public class Member
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int Dni { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public ICollection<ClassMember> ClassMembers { get; set; } = new List<ClassMember>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        
        public MembershipStatus MembershipStatus { get; set; }
        

    public Member(string firstName, string lastName, int age, int dni, int phoneNumber, string address, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Dni = dni;
            PhoneNumber = phoneNumber;
            Address = address;
            Email = email;
        }

        protected Member() { }

    }
}