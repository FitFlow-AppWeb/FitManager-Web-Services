   using System.Collections.Generic;
   using System.Threading.Tasks;
   using Microsoft.EntityFrameworkCore;
   using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
   using FitManager_Web_Services.Employees.Domain.Repositories;
   using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
   
   namespace FitManager_Web_Services.Employees.Infrastructure.Repositories
   {
       public class SpecialtyRepository : ISpecialtyRepository
       {
           private readonly AppDbContext _context;
   
           public SpecialtyRepository(AppDbContext context)
           {
               _context = context;
           }
   
           // Obtener todas las especialidades
           public async Task<IEnumerable<Specialty>> GetAllAsync()
           {
               return await _context.Specialties.ToListAsync();
           }
   
           // Obtener especialidad por ID
           public async Task<Specialty?> GetByIdAsync(int id)
           {
               return await _context.Specialties.FirstOrDefaultAsync(s => s.Id == id);
           }
   
           // Agregar una nueva especialidad
           public async Task AddAsync(Specialty specialty)
           {
               await _context.Specialties.AddAsync(specialty);
           }
       }
   }
    