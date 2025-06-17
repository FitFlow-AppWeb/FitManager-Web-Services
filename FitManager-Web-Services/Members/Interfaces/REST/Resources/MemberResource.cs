// Members/Interfaces/REST/Resources/MemberResource.cs

namespace FitManager_Web_Services.Members.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource returned for a member via the REST API.
    /// This record type serves as a Data Transfer Object (DTO) for outgoing data
    /// from the application to the client when a member's information is requested.
    /// It includes core member details and an optional associated membership status resource.
    /// </summary>
    /// <param name="Id">The unique identifier of the member.</param>
    /// <param name="FirstName">The first name of the member.</param>
    /// <param name="LastName">The last name of the member.</param>
    /// <param name="Age">The age of the member.</param>
    /// <param name="Dni">The DNI (National Identity Document) of the member.</param>
    /// <param name="PhoneNumber">The phone number of the member.</param>
    /// <param name="Address">The address of the member.</param>
    /// <param name="Email">The email address of the member.</param>
    /// <param name="MembershipStatus">The associated membership status resource, if available.</param>
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