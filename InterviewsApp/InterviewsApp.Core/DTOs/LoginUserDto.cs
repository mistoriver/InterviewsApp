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
        [Required(ErrorMessage = "Loc.Message.Login.LoginIsRequired")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required(ErrorMessage = "Loc.Message.Login.PasswordIsRequired")]
        public string Password { get; set; }
    }
}
