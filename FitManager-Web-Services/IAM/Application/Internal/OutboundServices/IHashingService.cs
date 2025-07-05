namespace FitManager_Web_Services.Users.Application.Internal.OutboundServices
{
    /// <summary>
    /// Interface for hashing service that computes and verifies password hashes.
    /// </summary>
    public interface IHashingService
    {
        /// <summary>
        /// Hashes a plain-text password.
        /// </summary>
        string HashPassword(string password);

        /// <summary>
        /// Verifies a plain-text password against a stored hash.
        /// </summary>
        bool VerifyPassword(string password, string passwordHash);
    }
}
