using CesiZenBackEnd.Core.Entities;

namespace CesiZenBackEnd.Infrastructure.Repositories.Abstraction
{
    public interface IInformationRepository
    {
        Task<IEnumerable<PageInformation>> GetAllAsync();
        Task<PageInformation?> GetByIdAsync(int id);
        Task AddAsync(PageInformation page);
        Task UpdateAsync(PageInformation page);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}