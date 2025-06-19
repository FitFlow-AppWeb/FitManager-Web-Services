using System;
using System.ComponentModel.DataAnnotations;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Resources
{
    public record CreatePurchaseDetailResource(
        [Required] int ItemTypeId,
        [Required] float UnitPrice,
        [Required] int Quantity,
        [Required] DateTime LastMaintenanceDate,
        [Required] DateTime NextMaintenanceDate,
        [Required] string Status,
        [Required] int EmployeeId
    );
}