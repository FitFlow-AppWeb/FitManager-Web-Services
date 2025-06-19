using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;
namespace FitManager_Web_Services.Finances.Interfaces.REST.Resources
{
    public record CreateSupplyPurchaseResource(
        [Required] DateTime Date,
        [Required] float Amount,
        [Required] string Method,
        [Required] string Currency,
        [Required] string VendorName,
        List<CreatePurchaseDetailResource>? PurchaseDetails
    );
}