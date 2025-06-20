﻿using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Employees.Domain.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<Employee?> GetByDniAsync(int dni);
    }
}