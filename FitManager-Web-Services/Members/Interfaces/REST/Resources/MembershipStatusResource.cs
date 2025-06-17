// Members/Interfaces/REST/Resources/MembershipStatusResource.cs



namespace FitManager_Web_Services.Members.Interfaces.REST.Resources 
{
    /// <summary>
    /// Represents the resource returned for a membership status via the REST API.
    /// This record type serves as a Data Transfer Object (DTO) for outgoing data,
    /// encapsulating the details of a member's subscription status.
    /// It includes the associated membership type resource to provide complete information.
    /// </summary>
    /// <param name="Id">The unique identifier of the membership status.</param>
    /// <param name="StartDate">The start date of the membership period.</param>
    /// <param name="EndDate">The end date of the membership period.</param>
    /// <param name="Status">The current status of the membership (e.g., "Active", "Expired").</param>
    /// <param name="MembershipTypeId">The ID of the associated membership type.</param>
    /// <param name="MembershipType">The associated membership type resource, if available.</param>
    public record MembershipStatusResource(
        int Id,
        DateTime StartDate,
        DateTime EndDate,
        string Status,
        int MembershipTypeId,
        MembershipTypeResource? MembershipType
    );
}