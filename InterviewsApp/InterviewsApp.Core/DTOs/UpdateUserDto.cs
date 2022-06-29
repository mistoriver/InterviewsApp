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
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина имени должна быть в пределах от 3 до 50 символов")]
        public string Name { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Длина логина должна быть в пределах от 5 до 20 символов")]
        public string Password { get; set; }

        /// <summary>
        /// Флаг активности
        /// </summary>
        public bool IsActive { get; set; }
    }
}
