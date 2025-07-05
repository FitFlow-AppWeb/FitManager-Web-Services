using FitManager_Web_Services.Users.Application.Internal.OutboundServices;
using BCryptNet = BCrypt.Net.BCrypt;

namespace FitManager_Web_Services.Users.Application.Internal.OutboundServices
{
    /// <summary>
    /// Implements password hashing and verification using BCrypt.
    /// </summary>
    public class HashingService : IHashingService
    {
        /// <inheritdoc />
        public string HashPassword(string password)
        {
            return BCryptNet.HashPassword(password);
        }

        /// <inheritdoc />
        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCryptNet.Verify(password, passwordHash);
        }
    }
}
