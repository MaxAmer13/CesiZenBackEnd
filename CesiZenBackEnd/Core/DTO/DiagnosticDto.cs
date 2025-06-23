using System.Runtime.InteropServices.JavaScript;

namespace CesiZenBackEnd.Core.DTO;

public class DiagnosticDto
{
    public int IdDiagnostic { get; set; }
    public int ScoreTotal { get; set; }
    public DateTime DateDiagnostic { get; set; }
    
    public int UtilisateurId { get; set; }
}