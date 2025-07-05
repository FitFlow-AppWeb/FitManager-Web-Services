namespace FitManager_Web_Services.IAM.Domain.Model.Queries
{
    /// <summary>
    /// Represents a query to retrieve a user by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the user to retrieve.</param>
    public record GetUserByIdQuery(int Id);
}
