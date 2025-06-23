using CesiZenBackEnd.Core.DTO;
using CesiZenBackEnd.Core.Entities;
using CesiZenBackEnd.Services;

namespace CesiZenBackEnd.Infrastructure.Repositories.Abstraction
{
    public interface IDiagnosticRepository
    {
        Task AddAsync(Diagnostic diagnostic);
        
        Task<IEnumerable<Diagnostic>>  GetAllAsync();
        Task<IEnumerable<Diagnostic>> GetByUserIdAsync(int userId);
        Task<Diagnostic?> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}