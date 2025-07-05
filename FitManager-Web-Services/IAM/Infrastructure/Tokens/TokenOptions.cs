namespace FitManager_Web_Services.IAM.Infrastructure.Tokens;

/// <summary>
/// Holds minimal JWT configuration values.
/// </summary>
public class TokenOptions
{
    public string Secret { get; set; } = default!;
}