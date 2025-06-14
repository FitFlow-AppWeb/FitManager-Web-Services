

namespace FitManager_Web_Services.Members.Interfaces.REST.Resources
{
    public record MemberResource(
        int Id,
        string FirstName,
        string LastName,
        int Age,
        int Dni,
        int PhoneNumber,
        string Address,
        string Email,
        MembershipStatusResource? MembershipStatus
    );
}