namespace CesiZenBackEnd.Core.DTO;

public class InformationPageDto
{
    
    public int InformationPageId { get; set; }
    public string Titre { get; set; } = null!;
    
    public string Contenu { get; set; } = null!;
}