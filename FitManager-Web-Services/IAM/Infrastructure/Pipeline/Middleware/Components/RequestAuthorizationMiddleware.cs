using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FitManager_Web_Services.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using FitManager_Web_Services.IAM.Domain.Repositories;
using FitManager_Web_Services.IAM.Application.Internal.OutboundServices;

namespace FitManager_Web_Services.IAM.Infrastructure.Pipeline.Middleware.Components
{
    /// <summary>
    /// Middleware that checks for JWT in the Authorization header and sets the User in HttpContext.Items.
    /// </summary>
    public class RequestAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IUserRepository userRepository,
            ITokenService tokenService)
        {
            // Skip if endpoint has [AllowAnonymous]
            var endpoint = context.GetEndpoint();
            var allowAnonymous = endpoint?.Metadata.OfType<AllowAnonymousAttribute>().Any() ?? false;
            if (allowAnonymous)
            {
                await _next(context);
                return;
            }

            // Check Authorization header
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader[7..].Trim();
                var userId = await tokenService.ValidateToken(token);
                if (userId.HasValue)
                {
                    var user = await userRepository.GetByIdAsync(userId.Value);
                    context.Items["User"] = user;
                }
            }

            if (context.Items["User"] == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next(context);
        }
    }
}
