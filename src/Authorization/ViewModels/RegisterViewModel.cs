using System.ComponentModel.DataAnnotations;

namespace Authorization.ViewModels
{
    public class RegisterViewModel
    {
        
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;

        [Required]
        public string Username { get; set; } = default!;
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
        
        public string? Firstname { get; set; }
        
        public string? Lastname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; } = default!;

    }
}