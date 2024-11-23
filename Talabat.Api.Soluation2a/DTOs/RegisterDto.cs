using System.ComponentModel.DataAnnotations;

namespace Talabat.Api.Soluation2a.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        //[RegularExpression("")]
        public string Password { get; set; }
    }
}
