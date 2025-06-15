// Members/Domain/Model/Commands/UpdateMemberCommand.cs



namespace FitManager_Web_Services.Members.Domain.Model.Commands
{
    /// <summary>
    /// Represents a command to update an existing member.
    /// This record type is an immutable data transfer object (DTO) that carries the unique identifier
    /// of the member to be updated, along with potentially updated information.
    /// Nullable types are used for properties that may not always be provided for an update,
    /// allowing for partial updates.
    /// It is processed by a command handler (e.g., <see cref="Application.Internal.CommandServices.MemberCommandService"/>).
    /// </summary>
    /// <param name="Id">The unique identifier of the member to be updated.</param>
    /// <param name="FirstName">The new first name of the member.</param>
    /// <param name="LastName">The new last name of the member.</param>
    /// <param name="Age">The new age of the member.</param>
    /// <param name="Dni">The new DNI (National Identity Document) of the member.</param>
    /// <param name="PhoneNumber">The new phone number of the member.</param>
    /// <param name="Address">The new address of the member.</param>
    /// <param name="Email">The new email address of the member.</param>
    /// <param name="StartDate">The optional new start date for the member's membership status.</param>
    /// <param name="EndDate">The optional new end date for the member's membership status.</param>
    /// <param name="Status">The optional new status string for the member's membership (e.g., "Active", "Expired").</param>
    /// <param name="MembershipTypeId">The optional new ID of the membership type associated with the member's membership status.</param>
    public record UpdateMemberCommand(
        int Id, 
        string FirstName,
        string LastName,
        int Age,
        int Dni,
        int PhoneNumber, 
        string Address,
        string Email,
        DateTime? StartDate,
        DateTime? EndDate,
        string? Status,
        int? MembershipTypeId
    );
}