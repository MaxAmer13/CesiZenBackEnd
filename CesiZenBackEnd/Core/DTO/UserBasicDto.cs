namespace CesiZenBackEnd.Core.DTO;

public class UserBasicDto
{
    public int Id { get; set; }

    public string Nom { get; set; }

    public string Prenom { get; set; }

    public string Email { get; set; }

    public string Adresse { get; set; }

    public bool EstActifUtil { get; set; }

    public DateTime DateCreation { get; set; }

    public int IdRole { get; set; }
}
