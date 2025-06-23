namespace CesiZenBackEnd.Infrastructure.Data;

public class JwtSettings
{
    public string SecretKey { get; set; } = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...\n";
    public int ExpirationMinutes { get; set; }
    public string Issuer { get; set; } = "CesiZenApp";
    public string Audience { get; set; } = "CesiZenUser";
}