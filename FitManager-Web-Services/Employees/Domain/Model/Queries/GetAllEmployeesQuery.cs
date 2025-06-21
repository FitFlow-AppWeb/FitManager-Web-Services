namespace FitManager_Web_Services.Employees.Domain.Model.Queries
{
    /// <summary>
    /// Represents a query to retrieve all employees.
    /// </summary>
    /// <remarks>
    /// This record type is an immutable data transfer object (DTO) used to signal the intention
    /// to fetch all employee records from the system. It is processed by a query handler
    /// (e.g., <see cref="Application.Internal.QueryServices.EmployeeQueryService"/>).
    /// </remarks>
    public record GetAllEmployeesQuery();
}