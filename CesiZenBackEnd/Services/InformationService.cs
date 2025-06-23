using CesiZenBackEnd.Core.DTO;
using CesiZenBackEnd.Core.Entities;
using CesiZenBackEnd.Infrastructure.Repositories.Abstraction;
using CesiZenBackEnd.Services.Abstraction;

namespace CesiZenBackEnd.Services;

public class InformationService : IInformationService
{
    private readonly IUnitOfWork _unitOfWork;

    public InformationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<InformationPageDto>> GetAllPagesAsync()
    {
        var pages = await _unitOfWork.Informations.GetAllAsync();
        return pages.Select(p => new InformationPageDto
        {
            InformationPageId = p.Id,
            Titre = p.Titre,
            Contenu = p.Contenu
        });
    }

    public async Task<InformationPageDto?> GetPageByIdAsync(int id)
    {
        var page = await _unitOfWork.Informations.GetByIdAsync(id);
        if (page == null) return null;

        return new InformationPageDto
        {
            InformationPageId = page.Id,
            Titre = page.Titre,
            Contenu = page.Contenu
        };
    }

    public async Task<int> CreatePageAsync(CreateInformationPageDto dto)
    {
        var page = new PageInformation
        {
            Titre = dto.Titre,
            Contenu = dto.Contenu
        };

        await _unitOfWork.Informations.AddAsync(page);
        await _unitOfWork.SaveChangesAsync();

        return page.Id;
    }

    public async Task<bool> UpdatePageAsync(int id, InformationPageDto dto)
    {
        var page = await _unitOfWork.Informations.GetByIdAsync(id);
        if (page == null) return false;

        page.Titre = dto.Titre;
        page.Contenu = dto.Contenu;

        _unitOfWork.Informations.UpdateAsync(page);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeletePageAsync(int id)
    {
        var page = await _unitOfWork.Informations.GetByIdAsync(id);
        if (page == null) return false;

        _unitOfWork.Informations.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
