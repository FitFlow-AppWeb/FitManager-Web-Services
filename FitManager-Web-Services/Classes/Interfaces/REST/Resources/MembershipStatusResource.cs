public record MembershipStatusResource(
    int Id,
    DateTime StartDate,
    DateTime? EndDate,
    string Status,
    int MembershipTypeId
    );