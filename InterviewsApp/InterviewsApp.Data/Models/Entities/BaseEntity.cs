using System;

namespace InterviewsApp.Data.Models.Entities
{
    /// <summary>
    /// Базовый класс сущности
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Уникальный идентификатор сущности
        /// </summary>
        public Guid Id { get; init; }
    }
}
