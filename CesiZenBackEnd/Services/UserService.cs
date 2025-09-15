using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using CesiZenBackEnd.Core.DTO;
using CesiZenBackEnd.Core.Entities;
using CesiZenBackEnd.Core.Enum;
using CesiZenBackEnd.Infrastructure.Repositories.Abstraction;
using CesiZenBackEnd.Services.Abstraction;
using Microsoft.IdentityModel.Tokens;


namespace CesiZenBackEnd.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResultDto> RegisterAsync(RegisterUserDto dto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new ArgumentException("Email déjà utilisé.");

        var newUser = new Utilisateur
        {
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Prenom = dto.Prenom,
            Nom = dto.Nom,
            RoleId = dto.RoleId.Value // <= IMPORTANT
        };

        await _userRepository.AddAsync(newUser);
        await _userRepository.SaveChangesAsync();

        return new LoginResultDto
        {
            Success = true,
            Id = newUser.Id,
            Email = newUser.Email,
            Prenom = newUser.Prenom,
            Nom = newUser.Nom,
        };
    }

    public async Task<UserBasicDto?> GetUserById(int userId)
    {
        var user = await _unitOfWork.Users.GetUserById(userId);
        if (user == null) return null;

        return new UserBasicDto()
        {
            Id = user.Id,
            Nom = user.Nom,
            Prenom = user.Prenom,
            Email = user.Email,
            Adresse = user.Adresse,
            EstActifUtil = user.Active,
            DateCreation = user.DateCreation,
            IdRole = user.RoleId
        };
    }



    public async Task<LoginResultDto> LoginAsync(LoginUserDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            return new LoginResultDto { Success = false, ErrorCode = "AUTH_FAILED" };
        }

        return new LoginResultDto
        {
            Success = true,
            Id = user.Id,
            Email = user.Email,
            Prenom = user.Prenom,
            Nom = user.Nom
        };
    }

}