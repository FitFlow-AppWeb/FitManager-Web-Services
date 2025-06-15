// Members/Domain/Model/Queries/GetAllMembershipTypesQuery.cs

namespace FitManager_Web_Services.Members.Domain.Model.Queries
{
    /// <summary>
    /// Represents a query to retrieve all membership types.
    /// This record type is an immutable data transfer object (DTO) used to signal the intention
    /// to fetch all available membership types. It is processed by a query handler
    /// (e.g., <see cref="Application.Internal.QueryServices.MembershipTypeQueryService"/>).
    /// </summary>
    public record GetAllMembershipTypesQuery();
}