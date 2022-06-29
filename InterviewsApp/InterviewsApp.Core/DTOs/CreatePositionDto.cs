using System;
using System.ComponentModel.DataAnnotations;

namespace InterviewsApp.Core.DTOs
{
    public class CreatePositionDto
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [Required(ErrorMessage = "Поле Название позиции является обязательным для заполнения")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Длина названия позиции должна быть в пределах от 1 до 50 символов")]
        public string Name { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        [Required(ErrorMessage = "Поле Город является обязательным для заполнения")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Длина названия города должна быть в пределах от 1 до 50 символов")]
        public string City { get; set; }

        /// <summary>
        /// Уникальный идентификатор компании
        /// </summary>
        [Required]
        public Guid CompanyId { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        [Required]
        public Guid UserId { get; set; }
    }
}
