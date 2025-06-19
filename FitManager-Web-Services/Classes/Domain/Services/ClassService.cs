using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;
using FitManager_Web_Services.Classes.Domain.Services;

namespace FitManager_Web_Services.Classes.Domain.Services;

public class ClassService : IClassService
{
    private readonly IClassRepository _classRepository;

    public ClassService(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<Class> CreateClassAsync(string name, string description, string type, int capacity, DateTime startDate, int duration, string status, int employeeId)
    {
        var fitnessClass = new Class(name, description, type, capacity, startDate, duration, status, employeeId);
        await _classRepository.AddAsync(fitnessClass);
        return fitnessClass;
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

    public async Task<Class?> GetClassByIdAsync(int id) => await _classRepository.GetByIdAsync(id);

    public async Task<IEnumerable<Class>> GetAllClassesAsync() => await _classRepository.ListAsync();

    public async Task<IEnumerable<Class>> GetClassesByTypeAsync(string type) => await _classRepository.FindByTypeAsync(type);

    public async Task<IEnumerable<ClassMember>> GetClassMembersAsync(int classId) => await _classRepository.GetMembersByClassIdAsync(classId);
}