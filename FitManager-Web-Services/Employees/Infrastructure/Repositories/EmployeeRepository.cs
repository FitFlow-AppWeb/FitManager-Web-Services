using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace FitManager_Web_Services.Employees.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los empleados
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Certifications)  // Incluir las certificaciones
                .Include(e => e.Specialties)     // Incluir las especialidades
                .ToListAsync();
        }

        // Obtener un empleado por ID
        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Certifications)
                .Include(e => e.Specialties)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        // Obtener un empleado por DNI
        public async Task<Employee?> GetByDniAsync(int dni)
        {
            return await _context.Employees
                .Include(e => e.Certifications)
                .Include(e => e.Specialties)
                .FirstOrDefaultAsync(e => e.Dni == dni);
        }

        // Agregar un nuevo empleado
        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        // Actualizar un empleado existente
        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
        }

        // Eliminar un empleado por ID
        public async Task DeleteAsync(int id)
        {
            var employee = await GetByIdAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
        }
    }
}
