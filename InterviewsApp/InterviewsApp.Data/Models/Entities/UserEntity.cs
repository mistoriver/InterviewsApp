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
        public string Name { get; set; } = null!;

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; } = null!;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// Флаг активности
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Навигационное свойство для связи с сущностью <see cref="PositionEntity"/>
        /// </summary>
        public List<PositionEntity> Positions { get; set; } = new();
    }
}
