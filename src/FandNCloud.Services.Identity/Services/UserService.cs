using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FandNCloud.Common.Auth;
using FandNCloud.Common.Exceptions;
using FandNCloud.Services.Identity.Domain.Models;
using FandNCloud.Services.Identity.Domain.Repositories;
using FandNCloud.Services.Identity.Domain.Services;
using FandNCloud.Services.Identity.Exceptions;
using Microsoft.AspNetCore.Http;
using RefreshToken = FandNCloud.Services.Identity.Domain.Models.RefreshToken;

namespace FandNCloud.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IInvalidTokenRepository _invalidTokenRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;

        public UserService(IUserRepository userRepository,
            IInvalidTokenRepository invalidTokenRepository, 
            IRefreshTokenRepository refreshTokenRepository,
            IEncrypter encrypter,
            IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _invalidTokenRepository = invalidTokenRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
        }

        public async Task<User> RegisterAsync(string email, string password, string firstName, string lastName)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new HttpResponseException(StatusCodes.Status400BadRequest, $"Email: '{email}' is already in use.");
            }
            user = new User(email, firstName, lastName);
            user.SetPassword(password, _encrypter);
            await _userRepository.AddAsync(user);
            var userEntity = await _userRepository.GetAsync(user.Email);
            return userEntity;
        }

        public async Task<JsonWebToken> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new HttpResponseException(StatusCodes.Status401Unauthorized, "Invalid credentials");
            }
            if (!user.ValidatePassword(password, _encrypter))
            {
                throw new HttpResponseException(StatusCodes.Status401Unauthorized, "Invalid credentials");
            }
            var jwt = _jwtHandler.Create(user.Id);
            await _refreshTokenRepository.AddAsync(new RefreshToken()
            {
                UserId = user.Id,
                Token = jwt.RefreshToken.Value,
                Expires = (new DateTime(1970, 1, 1)).AddSeconds(jwt.RefreshToken.Expires)
            });
            return jwt;
        }
        
        // Add error messages
        public async Task<JsonWebToken> RefreshAsync(string accessToken, string refreshToken)
        {
            var userId = _jwtHandler.RetrieveUserIdFromAccessToken(accessToken);
            if (userId == Guid.Empty)
                throw new HttpResponseException(StatusCodes.Status400BadRequest, "Invalid access_token");
            var refreshTokenEntity = await _refreshTokenRepository.GetByUserIdAndToken(userId, refreshToken);
            if (!refreshTokenEntity.IsValid)
                throw new HttpResponseException(StatusCodes.Status400BadRequest, "Invalid refresh_token");
            refreshTokenEntity.IsUsed = true;
            await _refreshTokenRepository.UpdateAsync(refreshTokenEntity);
            return _jwtHandler.Create(userId);
        }

        public async Task LogoutAsync(string accessToken, string refreshToken)
        {
            var userId = _jwtHandler.RetrieveUserIdFromAccessToken(accessToken);
            if (userId == Guid.Empty)
                throw new HttpResponseException(StatusCodes.Status400BadRequest, "Invalid access_token");
            await _invalidTokenRepository.AddAsync(new InvalidToken()
            {
                UserId = userId,
                Token = accessToken
            });
            var refreshTokenEntity = await _refreshTokenRepository.GetByUserIdAndToken(userId, refreshToken);
            if (!refreshTokenEntity.IsValid)
                throw new HttpResponseException(StatusCodes.Status400BadRequest, "Invalid refresh_token");
            await _refreshTokenRepository.DeleteAsync(refreshTokenEntity);
        }
    }
}