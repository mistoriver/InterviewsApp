using System.Collections.Generic;

namespace InterviewsApp.Data.Models.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; init; } = null!;

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; init; } = null!;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; init; } = null!;

        /// <summary>
        /// Флаг активности
        /// </summary>
        public bool IsActive { get; init; }

        /// <summary>
        /// Навигационное свойство для связи с сущностью <see cref="PositionEntity"/>
        /// </summary>
        public List<PositionEntity> Positions { get; init; } = new();
    }
}
