using InterviewsApp.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.DTOs
{
    public class UpdateInterviewDto
    {
        /// <summary>
        /// Уникальный идентификатор сущности
        /// </summary>
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        /// <summary>
        /// Дата проведения
        /// </summary>
        [Required(ErrorMessage = "Поле Дата собеседования является обязательным для заполнения")]
        [DateInFuture(ErrorMessage = "Дата собеседования должна быть в будущем")]
        public DateTime Date { get; set; }
    }
}
