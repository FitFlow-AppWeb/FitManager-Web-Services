
using System; 

namespace FitManager_Web_Services.Members.Domain.Model.Commands
{
    public record CreateMemberCommand(
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