using System;
using System.ComponentModel.DataAnnotations;

namespace InterviewsApp.Core.DTOs
{
    public class CreatePositionDto
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [Required(ErrorMessage = "Loc.Message.PositionNameRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Loc.Message.PositionNameSize")]
        public string Name { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        [Required(ErrorMessage = "Loc.Message.PositionCityRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Loc.Message.PositionCitySize")]
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
