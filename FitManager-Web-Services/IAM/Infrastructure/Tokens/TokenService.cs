using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FitManager_Web_Services.Users.Application.Internal.OutboundServices;
using FitManager_Web_Services.Users.Domain.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FitManager_Web_Services.Users.Infrastructure.Tokens
{
    /// <summary>
    /// Generates and validates JWT tokens for authentication.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly TokenSettings _settings;
        private readonly byte[] _key;

        public TokenService(IOptions<TokenSettings> options)
        {
            _settings = options.Value;
            _key = Encoding.ASCII.GetBytes(_settings.Secret);
        }

        /// <inheritdoc />
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };
            var creds = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature);
            var token = tokenHandler.CreateJwtSecurityToken(
                subject: new ClaimsIdentity(claims),
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );
            return tokenHandler.WriteToken(token);
        }

        /// <inheritdoc />
        public Task<int?> ValidateToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return Task.FromResult<int?>(null);

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_key),
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, parameters, out var validatedToken);
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
                // invalid token
            }
            return Task.FromResult<int?>(null);
        }
    }
}
