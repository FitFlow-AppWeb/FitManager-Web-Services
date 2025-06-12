namespace FitManager_Web_Services.Members.Interfaces.REST.Resources
{
    public record CreateMemberResource(
        string FirstName,
        string LastName,
        int Age,
        int Dni,
        int PhoneNumber,
        string Address,
        string Email,
        DateTime StartDate, 
        DateTime EndDate,   
        string Status,      
        int MembershipTypeId 
    );
}