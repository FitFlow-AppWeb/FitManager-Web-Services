using FitManager_Web_Services.Users.Domain.Model;
using FitManager_Web_Services.Users.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Users.Interfaces.REST.Transform
{
    /// <summary>
    /// Provides transformation methods between User domain entities and User API resources.
    /// </summary>
    public static class UserTransformer
    {
        /// <summary>
        /// Converts a User domain entity to a UserResource for API responses.
        /// </summary>
        public static UserResource ToResource(User user) => new(
            user.Id,
            user.Email,
            user.Icon,
            user.Subscription
        );
        }
}
