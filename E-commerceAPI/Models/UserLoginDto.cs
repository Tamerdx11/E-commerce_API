using System.ComponentModel.DataAnnotations;

namespace E_commerceAPI.Models
{
    public class UserLoginDto
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
