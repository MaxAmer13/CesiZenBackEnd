namespace CesiZenBackEnd.Core.Enum;

public enum UserRole
{
    Utilisateur,
    Administrateur
}

public static class UserRoleExtensions
{
    public static string ToRoleName(this UserRole role)
    {
        return role switch
        {
            UserRole.Utilisateur => "Citoyen",
            UserRole.Administrateur => "Administrateur",
            _ => throw new ArgumentOutOfRangeException(nameof(role), $"Rôle inconnu : {role}")
        };
    }
}