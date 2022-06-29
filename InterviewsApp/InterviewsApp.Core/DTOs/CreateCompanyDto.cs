using System.ComponentModel.DataAnnotations;

namespace InterviewsApp.Core.DTOs
{
    public class CreateCompanyDto
    {
        /// <summary>
        /// Название
        /// </summary>
        [Required(ErrorMessage = "Поле Название компании является обязательным для заполнения")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Длина названия компании должна быть в пределах от 1 до 50 символов")]
        public string Name { get; set; }
    }
}
