using CesiZenBackEnd.Core.Entities;
using CesiZenBackEnd.Infrastructure.Data;
using CesiZenBackEnd.Infrastructure.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace CesiZenBackEnd.Infrastructure.Repositories
{
    public class InformationRepository : IInformationRepository
    {
        private readonly AppDbContext _context;

        public InformationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PageInformation>> GetAllAsync()
        {
            return await _context.PageInformations.ToListAsync();
        }

        public async Task<PageInformation?> GetByIdAsync(int id)
        {
            return await _context.PageInformations.FindAsync(id);
        }

        public async Task AddAsync(PageInformation page)
        {
            await _context.PageInformations.AddAsync(page);
        }

        public async Task UpdateAsync(PageInformation page)
        {
            _context.PageInformations.Update(page);
        }

        public async Task DeleteAsync(int id)
        {
            var page = await _context.PageInformations.FindAsync(id);
            Console.WriteLine(page);
            if (page != null)
            {
                _context.PageInformations.Remove(page);
                await _context.SaveChangesAsync(); 
            }
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}