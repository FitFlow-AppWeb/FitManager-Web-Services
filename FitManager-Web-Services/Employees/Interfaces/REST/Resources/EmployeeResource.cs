namespace FitManager_Web_Services.Employees.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource for an employee, used for data transfer objects (DTOs) in RESTful APIs.
    /// </summary>
    /// <param name="Id">The unique identifier of the employee.</param>
    /// <param name="FirstName">The first name of the employee.</param>
    /// <param name="LastName">The last name of the employee.</param>
    /// <param name="Age">The age of the employee.</param>
    /// <param name="Dni">The DNI (National Identity Document) of the employee.</param>
    /// <param name="PhoneNumber">The phone number of the employee.</param>
    /// <param name="Address">The address of the employee.</param>
    /// <param name="Email">The email address of the employee.</param>
    /// <param name="Password">The password of the employee. (Note: In a real-world scenario, passwords should ideally not be exposed in a resource DTO, or should be hashed/salted.)</param>
    /// <param name="Wage">The hourly wage or salary of the employee.</param>
    /// <param name="Role">The role or position of the employee within the organization (e.g., "Trainer", "Administrator").</param>
    /// <param name="WorkHours">The standard weekly work hours for the employee.</param>
    /// <param name="Certifications">An optional collection of <see cref="CertificationResource"/> representing the certifications held by the employee.</param>
    /// <param name="Specialties">An optional collection of <see cref="SpecialtyResource"/> representing the specialties of the employee.</param>
    public record EmployeeResource(
        int Id,
        string FirstName,
        string LastName,
        int Age,
        int Dni,
        int PhoneNumber,
        string Address,
        string Email,
        string Password,  
        float Wage,       
        string Role,       
        int WorkHours,    
        IEnumerable<CertificationResource>? Certifications,  
        IEnumerable<SpecialtyResource>? Specialties         
    );
}