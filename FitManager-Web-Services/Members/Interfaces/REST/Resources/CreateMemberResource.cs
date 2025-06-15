// Members/Interfaces/REST/Resources/CreateMemberResource.cs


namespace FitManager_Web_Services.Members.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource for creating a new member via the REST API.
    /// This record type serves as a Data Transfer Object (DTO) for incoming data
    /// from the client when creating a new member. It includes both member details
    /// and the initial membership status details.
    /// </summary>
    /// <param name="FirstName">The first name of the member.</param>
    /// <param name="LastName">The last name of the member.</param>
    /// <param name="Age">The age of the member.</param>
    /// <param name="Dni">The DNI (National Identity Document) of the member.</param>
    /// <param name="PhoneNumber">The phone number of the member.</param>
    /// <param name="Address">The address of the member.</param>
    /// <param name="Email">The email address of the member.</param>
    /// <param name="StartDate">The start date of the member's initial membership period.</param>
    /// <param name="EndDate">The end date of the member's initial membership period.</param>
    /// <param name="Status">The initial status of the member's membership (e.g., "Active", "Pending").</param>
    /// <param name="MembershipTypeId">The ID of the membership type for the initial membership status.</param>
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