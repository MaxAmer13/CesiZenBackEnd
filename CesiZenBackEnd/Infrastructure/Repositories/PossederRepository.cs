using System.Threading.Tasks;
using CesiZenBackEnd.Core.Entities;
using CesiZenBackEnd.Infrastructure.Data;
using CesiZenBackEnd.Infrastructure.Repositories.Abstraction;

namespace CesiZenBackEnd.Infrastructure.Repositories
{
    public class PossederRepository : IPossederRepository
    {
        private readonly AppDbContext _context;

        public PossederRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddLinkAsync(int diagnosticId, int evenementStressId)
        {
            var posseder = new Posseder
            {
                IdDiagnostic = diagnosticId,
                IdEvenement = evenementStressId
            };

            await _context.Posseder.AddAsync(posseder);
        }
    }
}