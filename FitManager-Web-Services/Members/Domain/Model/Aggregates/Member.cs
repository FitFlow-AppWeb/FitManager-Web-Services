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