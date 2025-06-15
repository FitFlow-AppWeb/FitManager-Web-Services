// Members/Domain/Model/Commands/CreateMemberCommand.cs


namespace FitManager_Web_Services.Members.Domain.Model.Commands
{
    /// <summary>
    /// Represents a command to create a new member.
    /// This record type is an immutable data transfer object (DTO) that carries all the necessary
    /// information from the application's external boundary (e.g., REST API) into the Domain/Application layer
    /// for processing by a command handler (e.g., <see cref="Application.Internal.CommandServices.MemberCommandService"/>).
    /// </summary>
    /// <param name="FirstName">The first name of the member.</param>
    /// <param name="LastName">The last name of the member.</param>
    /// <param name="Age">The age of the member.</param>
    /// <param name="Dni">The DNI (National Identity Document) of the member.</param>
    /// <param name="PhoneNumber">The phone number of the member.</param>
    /// <param name="Address">The address of the member.</param>
    /// <param name="Email">The email address of the member.</param>
    /// <param name="StartDate">The start date of the member's initial membership status.</param>
    /// <param name="EndDate">The end date of the member's initial membership status.</param>
    /// <param name="Status">The status of the member's initial membership (e.g., "Active", "Pending").</param>
    /// <param name="MembershipTypeId">The ID of the membership type associated with the initial membership status.</param>
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