using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Data.Models.Entities
{
    /// <summary>
    /// Собеседование
    /// </summary>
    public class InterviewEntity : BaseEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Дата проведения
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Путь к комментарию
        /// </summary>
        public string? PathToComment { get; set; }

        /// <summary>
        /// Уникальный идентификатор вакансии
        /// </summary>
        public Guid PositionId { get; set; }

        /// <summary>
        /// Навигационное свойство для связи с сущностью <see cref="PositionEntity"/>
        /// </summary>
        public PositionEntity Position { get; set; } = new();
    }
}
