namespace FitManager_Web_Services.Inventory.Domain.Model.Queries
{
    /// <summary>
    /// Represents a query to retrieve all items.
    /// This record is used as a DTO to signal the intention to fetch all items.
    /// It is processed by a query handler, such as <see cref="Application.Internal.QueryServices.ItemQueryService"/>.
    /// </summary>
    public record GetAllItemsQuery();
}