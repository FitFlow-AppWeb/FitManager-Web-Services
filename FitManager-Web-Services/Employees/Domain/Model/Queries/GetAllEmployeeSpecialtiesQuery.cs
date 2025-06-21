namespace FitManager_Web_Services.Employees.Domain.Model.Queries
{
    /// <summary>
    /// Represents a query to retrieve all distinct employee specialties.
    /// </summary>
    /// <remarks>
    /// This record type is an immutable data transfer object (DTO) used to signal the intention
    /// to fetch all available specialties that employees might possess. It is processed by a query handler
    /// (e.g., a <see cref="Application.Internal.QueryServices.SpecialtyQueryService"/>,
    /// or as part of a larger employee-related query service).
    /// </remarks>
    public record GetAllEmployeeSpecialtiesQuery();
}