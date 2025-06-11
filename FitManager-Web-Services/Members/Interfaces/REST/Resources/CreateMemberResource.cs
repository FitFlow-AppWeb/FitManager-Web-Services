using System.ComponentModel.DataAnnotations; 

namespace FitManager_Web_Services.Members.Interfaces.REST.Resources
{
    public record CreateMemberResource(
        [Required] string FirstName,
        [Required] string LastName,
        [Range(1, 150)] int Age,
        [Required] [Range(1, 999999999)] int Dni,
        [Required] [Range(1, 999999999)] int PhoneNumber,
        [Required] string Address,
        [Required] [EmailAddress] string Email
    );
}