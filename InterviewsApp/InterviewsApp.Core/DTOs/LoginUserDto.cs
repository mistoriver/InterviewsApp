using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
