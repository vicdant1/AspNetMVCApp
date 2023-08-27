using System.ComponentModel.DataAnnotations;

namespace RunGroopWebApp.Models.ViewModels
{
    public class CreateOrEditPersonViewModel
    {
        public int? Id { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Data de Nascimento")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Documento RG")]
        public string RG { get; set; }
    }
}
