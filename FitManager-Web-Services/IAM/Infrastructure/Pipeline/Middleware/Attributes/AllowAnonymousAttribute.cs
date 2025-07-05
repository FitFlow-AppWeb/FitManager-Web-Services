using System;

namespace FitManager_Web_Services.IAM.Infrastructure.Pipeline.Middleware.Attributes
{
    /// <summary>
    /// Marks endpoints that skip authentication.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    {
    }
}
