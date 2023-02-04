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
        [Required(ErrorMessage = "Loc.Message.InterviewNameRequired")]
        [MaxLength(100, ErrorMessage = "Loc.Message.InterviewNameSize")]
        public string Name { get; set; }

        /// <summary>
        /// Дата проведения
        /// </summary>
        [Required(ErrorMessage = "Loc.Message.InterviewDateRequired")]
        [DateInFuture(ErrorMessage = "Loc.Message.InterviewDateInFuture")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Уникальный идентификатор вакансии
        /// </summary>
        [Required]
        public Guid PositionId { get; set; }
    }
}
