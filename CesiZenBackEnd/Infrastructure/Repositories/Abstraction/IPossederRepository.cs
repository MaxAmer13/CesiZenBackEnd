namespace CesiZenBackEnd.Infrastructure.Repositories.Abstraction;

public interface IPossederRepository
{
    Task AddLinkAsync(int diagnosticId, int evenementStressId);
}