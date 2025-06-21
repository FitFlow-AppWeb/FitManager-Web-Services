using System;
using System.Collections.Generic;
using FitManager_Web_Services.Members.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Resources;

public record ClassResource(
    int Id,
    string Name,
    string Description,
    string Type,
    int Capacity,
    DateTime StartDate,
    int Duration,
    string Status,
    int EmployeeId,
    IEnumerable<MemberResource>? EnrolledMembers
);