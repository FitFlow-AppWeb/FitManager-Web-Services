// Members/Interfaces/REST/Resources/MembershipTypeResource.cs

namespace FitManager_Web_Services.Members.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource returned for a membership type via the REST API.
    /// This record type serves as a Data Transfer Object (DTO) for outgoing data,
    /// encapsulating the details of a specific membership plan.
    /// </summary>
    /// <param name="Id">The unique identifier of the membership type.</param>
    /// <param name="Name">The name of the membership type (e.g., "Premium", "Basic").</param>
    /// <param name="Description">A detailed description of the membership type.</param>
    /// <param name="Price">The price of the membership type.</param>
    /// <param name="Duration">The duration of the membership (e.g., "1 Month", "Annual").</param>
    /// <param name="Benefits">The benefits included with this membership type.</param>
    public record MembershipTypeResource(
        int Id,
        string Name,
        string Description,
        decimal Price, 
        string Duration,
        string Benefits
    );
}