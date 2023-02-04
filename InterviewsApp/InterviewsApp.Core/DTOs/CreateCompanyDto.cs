using System.ComponentModel.DataAnnotations;

namespace InterviewsApp.Core.DTOs
{
    public class CreateCompanyDto
    {
        /// <summary>
        /// Название
        /// </summary>
        [Required(ErrorMessage = "Loc.Message.CompanyNameRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Loc.Message.CompanyNameSize")]
        public string Name { get; set; }
    }
}
