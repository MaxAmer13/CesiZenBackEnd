namespace CesiZenBackEnd.Core.DTO;

public class CreateDiagnosticDto
{
    public int ScoreTotal { get; set; }
    public List<int> EventIds { get; set; } = new();
}