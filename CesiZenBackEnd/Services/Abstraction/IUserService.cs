using CesiZenBackEnd.Core.DTO;
using CesiZenBackEnd.Core.Entities;


namespace CesiZenBackEnd.Services.Abstraction;

public interface IUserService
{
    Task<LoginResultDto> LoginAsync(LoginUserDto dto);
    Task<LoginResultDto> RegisterAsync(RegisterUserDto dto);
    Task<LoginResultDto?> GetUserByIdAsync(int userId);
}
