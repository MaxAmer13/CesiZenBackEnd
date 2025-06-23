using CesiZenBackEnd.Infrastructure.Data;
using CesiZenBackEnd.Infrastructure.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace CesiZenBackEnd.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Utilisateur?> GetByEmailAsync(string email)
    {
        return await _context.Utilisateurs.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddAsync(Utilisateur utilisateur)
    {
        await _context.Utilisateurs.AddAsync(utilisateur);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
