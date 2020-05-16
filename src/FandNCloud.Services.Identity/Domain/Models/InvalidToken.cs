using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FandNCloud.Services.Identity.Domain.Models
{
    public class InvalidToken
    {
        [Key]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}