namespace CesiZenBackEnd.Infrastructure.Repositories.Abstraction;

public interface IUserRepository
{
    Task<Utilisateur?> GetByEmailAsync(string email);
    Task AddAsync(Utilisateur utilisateur);
    
    Task<Utilisateur?> GetUserById(int id);  // Ajoutez cette méthode
    
    Task SaveChangesAsync();
}
