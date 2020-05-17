using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FandNCloud.Services.Identity.Domain.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsUsed { get; set; }
        [NotMapped]
        public bool IsActive => DateTime.UtcNow <= Expires;
        [NotMapped]
        public bool IsValid => !IsUsed && IsActive;
    }
}