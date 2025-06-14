using System.ComponentModel.DataAnnotations; 

namespace FitManager_Web_Services.Members.Interfaces.REST.Resources
{
    public record UpdateMemberResource(
        [Required] string FirstName,
        [Required] string LastName,
        [Required] int Age,
        [Required] int Dni,
        [Required] int PhoneNumber, 
        [Required] string Address,
        [Required] string Email,
        DateTime? StartDate, 
        DateTime? EndDate,
        string? Status,
        int? MembershipTypeId
    );
}