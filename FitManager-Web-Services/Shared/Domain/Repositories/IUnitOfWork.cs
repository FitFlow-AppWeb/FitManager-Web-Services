using System.Threading.Tasks;

namespace FitManager_Web_Services.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}