/// <summary>
/// Represents the resource for a member, used for data transfer objects (DTOs) in RESTful APIs.
/// </summary>
/// <param name="Id">The unique identifier of the member.</param>
/// <param name="FirstName">The first name of the member.</param>
/// <param name="LastName">The last name of the member.</param>
/// <param name="Age">The age of the member.</param>
/// <param name="Dni">The DNI (National Identity Document) of the member.</param>
/// <param name="PhoneNumber">The phone number of the member.</param>
/// <param name="Address">The address of the member.</param>
/// <param name="Email">The email address of the member.</param>
/// <param name="MembershipStatus">The membership status of the member, represented by a <see cref="MembershipStatusResource"/>.</param>
public record MemberResource(
    int Id,
    string FirstName,
    string LastName,
    int Age,
    int Dni,
    int PhoneNumber,
    string Address,
    string Email,
    MembershipStatusResource MembershipStatus);