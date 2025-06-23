using System.Threading.Tasks;
using CesiZenBackEnd.Services.Abstraction;

namespace CesiZenBackEnd.Infrastructure.Repositories.Abstraction
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        
        IDiagnosticRepository Diagnostics { get; }
        
        IInformationRepository  Informations { get; }
        
        IPossederRepository Posseder { get; }

        Task SaveChangesAsync();
    }
}