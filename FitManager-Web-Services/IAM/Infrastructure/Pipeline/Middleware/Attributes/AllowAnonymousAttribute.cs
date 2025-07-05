using System;

namespace FitManager_Web_Services.Users.Infrastructure.Pipeline.Middleware.Attributes
{
    /// <summary>
    /// Marks endpoints that skip authentication.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    {
    }
}
