using CesiZenBackEnd.Core.DTO;

namespace CesiZenBackEnd.Services.Abstraction;

public interface IDiagnosticService
{
    Task<int> CreateDiagnosticAsync(CreateDiagnosticDto dto, int utilisateurId);
    Task<IEnumerable<DiagnosticDto>> GetDiagnosticsByUserIdAsync(int utilisateurId);
    
    Task<IEnumerable<DiagnosticDto>> GetAllDiagnosticsAsync();
}