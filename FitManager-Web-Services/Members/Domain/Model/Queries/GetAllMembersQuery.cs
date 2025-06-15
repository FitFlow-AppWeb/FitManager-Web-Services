// Members/Domain/Model/Queries/GetAllMembersQuery.cs

namespace FitManager_Web_Services.Members.Domain.Model.Queries
{
    /// <summary>
    /// Represents a query to retrieve all members.
    /// This record type is an immutable data transfer object (DTO) used to signal the intention
    /// to fetch all registered members. It is processed by a query handler
    /// (e.g., <see cref="Application.Internal.QueryServices.MemberQueryService"/>).
    /// </summary>
    public record GetAllMembersQuery(); 
}