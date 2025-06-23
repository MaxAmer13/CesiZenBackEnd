using System.Threading.Tasks;
using CesiZenBackEnd.Infrastructure.Data;
using CesiZenBackEnd.Infrastructure.Repositories.Abstraction;
using CesiZenBackEnd.Services.Abstraction;
using IUnitOfWork = CesiZenBackEnd.Infrastructure.Repositories.Abstraction.IUnitOfWork;

namespace CesiZenBackEnd.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IUserRepository Users { get; }
        public IDiagnosticRepository Diagnostics { get; }
        public IInformationRepository Informations { get; }
        public IPossederRepository Posseder { get; }

        public UnitOfWork(
            AppDbContext context,
            IUserRepository userRepository,
            IDiagnosticRepository diagnosticRepository,
            IInformationRepository informationRepository,
            IPossederRepository possederRepository)
        {
            _context = context;
            Users = userRepository;
            Diagnostics = diagnosticRepository;
            Informations = informationRepository;
            Posseder = possederRepository;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}