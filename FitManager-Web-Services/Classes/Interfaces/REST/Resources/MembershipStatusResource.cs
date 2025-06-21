/// <summary>
/// Represents the resource for a member's membership status, used for data transfer objects (DTOs) in RESTful APIs.
/// </summary>
/// <param name="Id">The unique identifier of the membership status record.</param>
/// <param name="StartDate">The date when the membership became active.</param>
/// <param name="EndDate">The date when the membership is set to expire. This can be null if the membership is ongoing or perpetual.</param>
/// <param name="Status">The current status of the membership (e.g., "Active", "Expired", "Paused").</param>
/// <param name="MembershipTypeId">The unique identifier of the type of membership (e.g., Annual, Monthly, Premium).</param>
public record MembershipStatusResource(
    int Id,
    DateTime StartDate,
    DateTime? EndDate,
    string Status,
    int MembershipTypeId
);