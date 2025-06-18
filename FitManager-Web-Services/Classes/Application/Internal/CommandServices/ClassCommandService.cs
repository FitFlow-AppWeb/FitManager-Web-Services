using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;
using FitManager_Web_Services.Classes.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Classes.Application.Internal.CommandServices;

public class ClassCommandService : IClassService
{
    private readonly IClassRepository _classRepository;

    public ClassCommandService(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<Class> CreateClassAsync(string name, string description, string type, int capacity, DateTime startDate, int duration, string status, int employeeId)
    {
        var newClass = new Class(name, description, type, capacity, startDate, duration, status, employeeId);
        await _classRepository.AddAsync(newClass);
        return newClass;
    }

    public async Task UpdateClassAsync(int id, string name, string description, string type, int capacity, DateTime startDate, int duration, string status, int employeeId)
    {
        var existingClass = await _classRepository.GetByIdAsync(id);
        if (existingClass == null) throw new Exception("Class not found");
        
        existingClass.Name = name;
        existingClass.Description = description;
        existingClass.Type = type;
        existingClass.Capacity = capacity;
        existingClass.StartDate = startDate;
        existingClass.Duration = duration;
        existingClass.Status = status;
        existingClass.EmployeeId = employeeId;
        
        await _classRepository.UpdateAsync(existingClass);
    }

    public async Task DeleteClassAsync(int id)
    {
        var existingClass = await _classRepository.GetByIdAsync(id);
        if (existingClass == null) throw new Exception("Class not found");
        
        await _classRepository.DeleteAsync(existingClass);
    }
    
    public async Task<IEnumerable<Class>> GetAllClassesAsync()
    {
        return await _classRepository.ListAsync();
    }

    public async Task<Class?> GetClassByIdAsync(int id)
    {
        return await _classRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Class>> GetClassesByTypeAsync(string type)
    {
        return await _classRepository.FindByTypeAsync(type);
    }

    public async Task<IEnumerable<ClassMember>> GetClassMembersAsync(int classId)
    {
        return await _classRepository.GetMembersByClassIdAsync(classId);
    }
}