using System.ComponentModel.DataAnnotations;

namespace RunGroopWebApp.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email requerido")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
