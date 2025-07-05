using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FitManager_Web_Services.IAM.Application.Internal.OutboundServices;
using FitManager_Web_Services.IAM.Domain.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FitManager_Web_Services.IAM.Infrastructure.Tokens
{
    /// <summary>
    /// Generates and validates JWT tokens for authentication.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly string _secret;
        private readonly byte[] _key;

        public TokenService(IOptions<TokenOptions> options)
        {
            _secret = options.Value.Secret ?? throw new ArgumentNullException(nameof(options.Value.Secret));
            _key = Encoding.ASCII.GetBytes(_secret);
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var creds = new SigningCredentials(
                new SymmetricSecurityKey(_key),
                SecurityAlgorithms.HmacSha256Signature);

            var token = tokenHandler.CreateJwtSecurityToken(
                subject: new ClaimsIdentity(claims),
                expires: DateTime.UtcNow.AddHours(2), // Tiempo fijo
                signingCredentials: creds
            );

            return tokenHandler.WriteToken(token);
        }

        public Task<int?> ValidateToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return Task.FromResult<int?>(null);

            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_key),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwt &&
                    jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var idClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (int.TryParse(idClaim, out var userId))
                        return Task.FromResult<int?>(userId);
                }
            }
            catch
            {
                // Token inválido
            }

            return Task.FromResult<int?>(null);
        }
    }
}
