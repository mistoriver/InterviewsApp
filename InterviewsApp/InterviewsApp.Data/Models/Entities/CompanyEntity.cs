using System.Collections.Generic;

namespace InterviewsApp.Data.Models.Entities
{
    /// <summary>
    /// Компания
    /// </summary>
    public class CompanyEntity : BaseEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; init; } = null!;

        /// <summary>
        /// Рейтинг
        /// </summary>
        public short Rating { get; init; }

        /// <summary>
        /// Навигационное свойство для связи с сущностью <see cref="PositionEntity"/>
        /// </summary>
        public List<PositionEntity> Positions { get; init; } = new();
    }
}
