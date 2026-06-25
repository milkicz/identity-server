namespace IdentityServer.Configuration;

public sealed class JwtSettings
{
    public const string SectionName = "Jwt";

    public required string Issuer { get; init; }
    public TimeSpan AccessTokenTTL { get; init; } = TimeSpan.FromMinutes(15);
    public TimeSpan RefreshTokenTTL { get; init; } = TimeSpan.FromDays(7);
}
