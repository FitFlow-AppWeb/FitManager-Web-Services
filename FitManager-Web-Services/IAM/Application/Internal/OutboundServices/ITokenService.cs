using System.Threading.Tasks;
using FitManager_Web_Services.IAM.Domain.Model;

namespace FitManager_Web_Services.IAM.Application.Internal.OutboundServices
{
    /// <summary>
    /// Interface for JWT token generation and validation.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generate a signed JWT for the given user.
        /// </summary>
        string GenerateToken(User user);

        /// <summary>
        /// Validate a token and retrieve the user ID if valid.
        /// </summary>
        Task<int?> ValidateToken(string token);
    }
}
