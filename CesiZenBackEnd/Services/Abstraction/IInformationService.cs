using CesiZenBackEnd.Core.DTO;

namespace CesiZenBackEnd.Services.Abstraction;

public interface IInformationService
{
    Task<IEnumerable<InformationPageDto>> GetAllPagesAsync();
    Task<InformationPageDto?> GetPageByIdAsync(int id);
    Task<int> CreatePageAsync(CreateInformationPageDto dto);
    Task<bool> UpdatePageAsync(int id, InformationPageDto dto);
    Task<bool> DeletePageAsync(int id);
}