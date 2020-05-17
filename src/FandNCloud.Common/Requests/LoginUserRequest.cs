using System.ComponentModel.DataAnnotations;

namespace FandNCloud.Common.Requests
{
    public class LoginUserRequest : IRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}