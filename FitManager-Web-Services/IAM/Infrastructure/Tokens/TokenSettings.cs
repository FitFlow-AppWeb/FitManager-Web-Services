namespace FitManager_Web_Services.IAM.Infrastructure.Tokens;

/// <summary>
/// Holds JWT configuration values.
/// </summary>
public class TokenSettings
{
    public string Secret { get; set; } = default!;
}
