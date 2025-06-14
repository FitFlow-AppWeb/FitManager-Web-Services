using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace FitManager_Web_Services.Employees.Infrastructure.Repositories
{
    public class CertificationRepository : ICertificationRepository
    {
        private readonly AppDbContext _context;

        public CertificationRepository(AppDbContext context)
        {
            _context = context;
        }

        // Implementación de GetAllAsync
        public async Task<IEnumerable<Certification>> GetAllAsync()
        {
            return await _context.Certifications.ToListAsync(); // Asegúrate de que el nombre de la entidad en DbContext sea correcto
        }

        public async Task<Certification?> GetByIdAsync(int id)
        {
            return await _context.Certifications.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Certification certification)
        {
            await _context.Certifications.AddAsync(certification);
        }
    }
}