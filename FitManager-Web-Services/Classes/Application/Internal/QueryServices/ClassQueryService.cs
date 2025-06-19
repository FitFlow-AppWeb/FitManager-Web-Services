using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;

namespace FitManager_Web_Services.Classes.Application.Internal.QueryServices;

public class ClassQueryService
{
    private readonly IClassRepository _classRepository;

    public ClassQueryService(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<IEnumerable<Class>> GetAllClassesAsync()
    {
        return await _classRepository.ListAsync();
    }

    public async Task<Class> GetClassByIdAsync(int id)
    {
        return await _classRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Class>> GetClassesByTypeAsync(string type)
    {
        return await _classRepository.FindByTypeAsync(type);
    }
}