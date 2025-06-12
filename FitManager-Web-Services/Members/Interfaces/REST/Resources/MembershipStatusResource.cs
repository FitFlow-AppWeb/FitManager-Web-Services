

namespace FitManager_Web_Services.Members.Interfaces.REST.Resources 
{
    public record MembershipStatusResource(
        int Id,
        DateTime StartDate,
        DateTime EndDate,
        string Status,
        int MembershipTypeId,
        MembershipTypeResource? MembershipType
    );
}