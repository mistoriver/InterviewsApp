using System.ComponentModel.DataAnnotations;

namespace InterviewsApp.Core.DTOs
{
    /// <summary>
    /// Транспортный объект для аутентификации пользователя
    /// </summary>
    public class LoginUserDto
    {

        /// <summary>
        /// Логин
        /// </summary>
        [Required (ErrorMessage = "Не указан логин")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
    }
}
