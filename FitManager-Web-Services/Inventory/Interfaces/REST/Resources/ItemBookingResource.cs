namespace FitManager_Web_Services.Inventory.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource returned for an item booking in the inventory module via the REST API.
    /// This DTO provides details about the reservation of an item for use in a specific class.
    /// </summary>
    /// <param name="Id">The unique identifier of the item booking.</param>
    /// <param name="ReservationDate">The date and time when the reservation was made.</param>
    /// <param name="UsageTime">The duration of the booking in minutes.</param>
    /// <param name="ClassId">The identifier of the class associated with the booking.</param>
    /// <param name="ItemId">The identifier of the item being reserved.</param>
    /// <param name="Item">The detailed resource of the reserved item, if available.</param>
    public record ItemBookingResource(
        int Id,
        DateTime ReservationDate,
        int UsageTime,
        int ClassId,
        int ItemId,
        ItemResource? Item
    );
}