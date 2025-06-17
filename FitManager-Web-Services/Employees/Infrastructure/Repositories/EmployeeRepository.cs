using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;
using System.Collections.Generic;

namespace FitManager_Web_Services.Employees.Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Employee?> GetByDniAsync(int dni)
        {
            return await _context.Employees
                .Include(e => e.Certifications)
                .Include(e => e.Specialties)
                .FirstOrDefaultAsync(e => e.Dni == dni);
        }

        public override async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Certifications)
                .Include(e => e.Specialties)
                .ToListAsync();
        }

        public override async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Certifications)
                .Include(e => e.Specialties)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}