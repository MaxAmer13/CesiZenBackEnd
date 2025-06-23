using CesiZenBackEnd.Core.Entities;
using CesiZenBackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CesiZenBackEnd.Infrastructure.Initializer;

public class RoleInitializer
{
    public static async Task SeedRolesAsync(AppDbContext context)
    {
        var roles = new[] { "Utilisateur", "Administrateur"};

        foreach (var roleName in roles)
        {
            var exists = await context.Roles.AnyAsync(r => r.libelRole == roleName);
            if (!exists)
            {
                await context.Roles.AddAsync(new Role { libelRole = roleName });
            }
        }

        await context.SaveChangesAsync();
    }
}