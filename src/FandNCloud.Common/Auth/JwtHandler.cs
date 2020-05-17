using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FandNCloud.Common.Auth
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtOptions _options;
        private readonly JwtHeader _jwtHeader;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IRefreshTokenFactory _refreshTokenFactory;

        public JwtHandler(IOptions<JwtOptions> options, IRefreshTokenFactory refreshTokenFactory)
        {
            _options = options.Value;
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(signingCredentials);
            _tokenValidationParameters = new TokenValidationParameters
            {
                // ValidateIssuer = true,
                // ValidateLifetime = true,
                // ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidIssuer = _options.Issuer,
                IssuerSigningKey = issuerSigningKey
            };
            _refreshTokenFactory = refreshTokenFactory;
        }

        public JsonWebToken Create(Guid userId)
        {
            return new JsonWebToken
            {
                AccessToken = CreateAccessToken(userId),
                RefreshToken = CreateRefreshToken()
            };
        }

        private RefreshToken CreateRefreshToken()
        {
            var refreshToken = _refreshTokenFactory.GenerateRefreshToken();
            var expires = DateTimeOffset.UtcNow.AddDays(1).ToUnixTimeSeconds();
            return new RefreshToken()
            {
                Value = refreshToken,
                Expires = expires
            };
        }

        private AccessToken CreateAccessToken(Guid userId)
        {
            var nowUtc = DateTimeOffset.UtcNow;
            var expires = nowUtc.AddMinutes(_options.ExpiryMinutes);
            var exp = expires.ToUnixTimeSeconds();
            var now = nowUtc.ToUnixTimeSeconds();
            var payload = new JwtPayload()
            {
                {"sub", userId},
                {"iss", _options.Issuer},
                {"iat", now},
                {"exp", exp},
                {"unique_name", userId}
            };
            var jwt = new JwtSecurityToken(_jwtHeader, payload);
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken()
            {
                Value = token,
                Expires = exp
            };
        }

        public Guid RetrieveUserIdFromAccessToken(string accessToken)
        {
            var validatedToken = GetPrincipalFromToken(accessToken);
            if (validatedToken == null)
                return Guid.Empty;
            var id = validatedToken.FindFirst(ClaimTypes.Name);
            return id == null ? Guid.Empty : Guid.Parse(id.Value);
        }
        
        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = _jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}