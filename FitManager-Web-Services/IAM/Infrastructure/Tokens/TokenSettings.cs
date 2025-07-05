namespace FitManager_Web_Services.Users.Infrastructure.Tokens;

/// <summary>
/// Holds JWT configuration values.
/// </summary>
public class TokenSettings
{
    public string Secret { get; set; } = default!;
}
