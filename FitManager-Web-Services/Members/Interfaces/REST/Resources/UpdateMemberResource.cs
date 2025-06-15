// Members/Interfaces/REST/Resources/UpdateMemberResource.cs

using System.ComponentModel.DataAnnotations; 

namespace FitManager_Web_Services.Members.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource for updating an existing member via the REST API.
    /// This record type serves as a Data Transfer Object (DTO) for incoming data
    /// from the client when updating a member. It uses nullable types for properties
    /// that might be partially updated, while core identification and essential
    /// fields are marked as required.
    /// </summary>
    /// <param name="FirstName">The new first name of the member. (Required)</param>
    /// <param name="LastName">The new last name of the member. (Required)</param>
    /// <param name="Age">The new age of the member. (Required)</param>
    /// <param name="Dni">The new DNI (National Identity Document) of the member. (Required)</param>
    /// <param name="PhoneNumber">The new phone number of the member. (Required)</param>
    /// <param name="Address">The new address of the member. (Required)</param>
    /// <param name="Email">The new email address of the member. (Required)</param>
    /// <param name="StartDate">The optional new start date for the member's membership status.</param>
    /// <param name="EndDate">The optional new end date for the member's membership status.</param>
    /// <param name="Status">The optional new status string for the member's membership (e.g., "Active", "Expired").</param>
    /// <param name="MembershipTypeId">The optional new ID of the membership type associated with the member's membership status.</param>
    public record UpdateMemberResource(
        [Required] string FirstName,
        [Required] string LastName,
        [Required] int Age,
        [Required] int Dni,
        [Required] int PhoneNumber, 
        [Required] string Address,
        [Required] string Email,
        DateTime? StartDate, 
        DateTime? EndDate,
        string? Status,
        int? MembershipTypeId
    );
}