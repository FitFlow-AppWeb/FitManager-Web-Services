// Members/Interfaces/REST/Resources/MembershipTypeResource.cs
namespace FitManager_Web_Services.Members.Interfaces.REST.Resources
{
    public record MembershipTypeResource(
        int Id,
        string Name,
        string Description,
        decimal Price,
        string Duration,
        string Benefits
    );
}