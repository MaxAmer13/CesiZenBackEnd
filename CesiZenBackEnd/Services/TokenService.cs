using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using CesiZenBackEnd.Core.DTO;
using CesiZenBackEnd.Core.Entities;
using CesiZenBackEnd.Infrastructure.Data;
using CesiZenBackEnd.Services.Abstraction;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace CesiZenBackEnd.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings _settings;

    public TokenService(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
    }

    public string GenerateToken(LoginResultDto dto)
    {
        if (dto is null) throw new ArgumentNullException(nameof(dto));
        if (string.IsNullOrWhiteSpace(_settings?.SecretKey))
            throw new SecurityTokenException("JWT secret key is missing");
        if (string.IsNullOrWhiteSpace(_settings?.Issuer))
            throw new SecurityTokenException("JWT issuer is missing");
        if (string.IsNullOrWhiteSpace(_settings?.Audience))
            throw new SecurityTokenException("JWT audience is missing");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, dto.Id.ToString()),
            new Claim(ClaimTypes.Email, dto.Email)
        };

        if (!string.IsNullOrWhiteSpace(dto.LibelRole))
        {
            claims.Add(new Claim(ClaimTypes.Role, dto.LibelRole));
        }
        else if (dto.RoleId.HasValue)
        {
            // facultatif: fallback sur l'id numérique
            claims.Add(new Claim(ClaimTypes.Role, dto.RoleId.Value.ToString()));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.ExpirationMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


}