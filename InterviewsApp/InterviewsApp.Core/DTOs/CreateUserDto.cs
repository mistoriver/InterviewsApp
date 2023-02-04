using System.ComponentModel.DataAnnotations;

namespace InterviewsApp.Core.DTOs
{
    /// <summary>
    /// Транспортный объект для создания пользователя
    /// </summary>
    public class CreateUserDto
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required(ErrorMessage = "Loc.Message.NameIsRequired")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Loc.Message.NameSize")]
        public string Name { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        [Required(ErrorMessage = "Loc.Message.LoginIsRequired")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Loc.Message.LoginSize")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required(ErrorMessage = "Loc.Message.PasswordIsRequired")]
        [MinLength(8, ErrorMessage = "Loc.Message.PasswordSize")]
        public string Password { get; set; }
    }
}
