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
        public string Name { get; init; } = null!;

        /// <summary>
        /// Дата проведения
        /// </summary>
        public DateTime Date { get; init; }

        /// <summary>
        /// Путь к комментарию
        /// </summary>
        public string? PathToComment { get; init; }

        /// <summary>
        /// Уникальный идентификатор вакансии
        /// </summary>
        public Guid PositionId { get; init; }

        /// <summary>
        /// Навигационное свойство для связи с сущностью <see cref="PositionEntity"/>
        /// </summary>
        public PositionEntity Position { get; init; } = new();
    }
}
