using System.ComponentModel.DataAnnotations;

namespace InterviewsApp.Core.DTOs
{
    /// <summary>
    /// Транспортный объект для обновления информации о пользователе
    /// </summary>
    public class UpdateUserDto
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Loc.Message.NameSize")]
        public string Name { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Loc.Message.PasswordSize")]
        public string Password { get; set; }

        /// <summary>
        /// Флаг активности
        /// </summary>
        public bool IsActive { get; set; }
    }
}
