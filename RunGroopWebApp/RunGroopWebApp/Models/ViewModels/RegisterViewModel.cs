using System.ComponentModel.DataAnnotations;

namespace RunGroopWebApp.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
