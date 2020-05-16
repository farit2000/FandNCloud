using System;
using System.Security.Cryptography;

namespace FandNCloud.Common.Auth
{
    public class RefreshTokenFactory : IRefreshTokenFactory
    {
        public string GenerateRefreshToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}