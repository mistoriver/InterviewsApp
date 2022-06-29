using InterviewsApp.Core.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace InterviewsApp.Core.DTOs
{
    /// <summary>
    /// Транспортный объект для создания собеседования
    /// </summary>
    public class CreateInterviewDto
    {
        /// <summary>
        /// Название
        /// </summary>
        [Required(ErrorMessage = "Поле Название собеседования является обязательным для заполнения")]
        [MaxLength(100, ErrorMessage = "Длина названия собеседования не должна превышать 100 символов")]
        public string Name { get; set; }

        /// <summary>
        /// Дата проведения
        /// </summary>
        [Required(ErrorMessage = "Поле Дата собеседования является обязательным для заполнения")]
        [DateInFuture(ErrorMessage = "Дата собеседования должна быть в будущем")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Уникальный идентификатор вакансии
        /// </summary>
        [Required]
        public Guid PositionId { get; set; }
    }
}
