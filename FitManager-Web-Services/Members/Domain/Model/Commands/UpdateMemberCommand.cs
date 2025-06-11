using System;

namespace FitManager_Web_Services.Members.Domain.Model.Commands
{
    public record UpdateMemberCommand(
        int Id, 
        string FirstName,
        string LastName,
        int Age,
        int Dni,
        int PhoneNumber,
        string Address,
        string Email
    );
}