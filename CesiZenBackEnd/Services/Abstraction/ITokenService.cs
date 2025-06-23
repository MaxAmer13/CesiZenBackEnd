using CesiZenBackEnd.Core.DTO;

namespace CesiZenBackEnd.Services.Abstraction;

public interface ITokenService
{
    string GenerateToken(LoginResultDto loginResultDto);
}