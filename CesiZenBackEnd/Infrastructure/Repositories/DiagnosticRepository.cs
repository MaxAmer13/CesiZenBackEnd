using CesiZenBackEnd.Core.Entities;
using CesiZenBackEnd.Infrastructure.Data;
using CesiZenBackEnd.Infrastructure.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace CesiZenBackEnd.Infrastructure.Repositories
{
    public class DiagnosticRepository : IDiagnosticRepository
    {
        private readonly AppDbContext _context;

        public DiagnosticRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Diagnostic diagnostic)
        {
            await _context.Diagnostics.AddAsync(diagnostic);
        }

        public async Task<IEnumerable<Diagnostic>> GetByUserIdAsync(int userId)
        {
            return await _context.Diagnostics
                .Where(d => d.IdUtilisateur == userId)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Diagnostic>> GetAllAsync()
        {
            return await _context.Diagnostics.ToListAsync();
        }


        public async Task<Diagnostic?> GetByIdAsync(int id)
        {
            return await _context.Diagnostics.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}