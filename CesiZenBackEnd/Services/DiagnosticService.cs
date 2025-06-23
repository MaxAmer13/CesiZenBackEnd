using CesiZenBackEnd.Core.DTO;
using CesiZenBackEnd.Core.Entities;
using CesiZenBackEnd.Services.Abstraction;
using CesiZenBackEnd.Infrastructure.Repositories;
using CesiZenBackEnd.Infrastructure.Repositories.Abstraction;

namespace CesiZenBackEnd.Services;

public class DiagnosticService : IDiagnosticService
{
    private readonly IUnitOfWork _unitOfWork;

    public DiagnosticService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CreateDiagnosticAsync(CreateDiagnosticDto dto, int userId)
    {
        var diagnostic = new Diagnostic
        {
            ScoreTotal = dto.ScoreTotal,
            IdUtilisateur = userId,
            DateDiagnostic = DateTime.UtcNow
        };

        // Ajout du diagnostic
        await _unitOfWork.Diagnostics.AddAsync(diagnostic);
        await _unitOfWork.SaveChangesAsync();

        // Ajout des liens dans la table intermédiaire Posseder
        foreach (var evenementId in dto.EventIds)
        {
            await _unitOfWork.Posseder.AddLinkAsync(diagnostic.Id, evenementId);
        }

        await _unitOfWork.SaveChangesAsync();

        return diagnostic.Id;
    }


    public async Task<IEnumerable<DiagnosticDto>> GetDiagnosticsByUserIdAsync(int userId)
    {
        var diagnostics = await _unitOfWork.Diagnostics.GetByUserIdAsync(userId);
        return diagnostics.Select(d => new DiagnosticDto
        {
            IdDiagnostic = d.Id,
            ScoreTotal = d.ScoreTotal,
            DateDiagnostic = d.DateDiagnostic
        });
    }
    
    public async Task<IEnumerable<DiagnosticDto>> GetAllDiagnosticsAsync()
    {
        var diagnostics = await _unitOfWork.Diagnostics.GetAllAsync();
    
        return diagnostics.Select(d => new DiagnosticDto
        {
            IdDiagnostic = d.Id,
            DateDiagnostic = d.DateDiagnostic,
            ScoreTotal = d.ScoreTotal,
            UtilisateurId = d.IdUtilisateur
        });
    }

}