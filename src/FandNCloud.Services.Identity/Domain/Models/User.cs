using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FandNCloud.Common.Exceptions;
using FandNCloud.Services.Identity.Domain.Database;
using FandNCloud.Services.Identity.Domain.Services;
using Microsoft.AspNetCore.Identity;

namespace FandNCloud.Services.Identity.Domain.Models
{
    // Add constraints 
    public class User : IdentityUser<Guid>
    {
        [Required(ErrorMessage = "first name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "password is required")]
        public string Password { get; set; }
        public string Salt { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<InvalidToken> InvalidTokens { get; set; }

        public User(string email, string firstName=null, string lastName=null) : base()
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ActioException("empty_user_email", 
                    "User email can not be empty.");
            }
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new ActioException("empty_first_name", 
                    "User name can not be empty.");
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ActioException("empty_last_name", 
                    "User name can not be empty.");
            }
            Email = email.ToLowerInvariant();
            FirstName = firstName;
            LastName = lastName;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ActioException("empty_password", 
                    "Password can not be empty.");
            }             
            Salt = encrypter.GetSalt();
            Password = encrypter.GetHash(password, Salt);
        }

        public bool ValidatePassword(string password, IEncrypter encrypter)
            => Password.Equals(encrypter.GetHash(password, Salt));


        /*public bool ValidateRefreshToken(string refreshToken)
            => RefreshTokens.Any(e => e.Token == refreshToken && e.IsValid);*/
    }
}