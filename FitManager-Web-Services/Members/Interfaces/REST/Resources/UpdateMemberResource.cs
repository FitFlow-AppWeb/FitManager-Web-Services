namespace FitManager_Web_Services.Members.Interfaces.REST.Resources
{
    public class UpdateMemberResource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        
        public int Dni { get; set; }
        
        public string Email { get; set; }
    }
}