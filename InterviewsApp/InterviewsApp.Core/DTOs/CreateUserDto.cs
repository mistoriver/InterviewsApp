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
        [Required(ErrorMessage = "Поле Имя является обязательным для заполнения")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина имени должна быть в пределах от 3 до 50 символов")]
        public string Name { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        [Required(ErrorMessage = "Поле Логин является обязательным для заполнения")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Длина логина должна быть в пределах от 5 до 20 символов")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required(ErrorMessage = "Поле Пароль является обязательным для заполнения")]
        [MinLength(8, ErrorMessage = "Длина пароля должна быть не меньше 8 символов")]
        public string Password { get; set; }
    }
}
