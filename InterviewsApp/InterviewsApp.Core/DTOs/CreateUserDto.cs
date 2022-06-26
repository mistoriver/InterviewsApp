using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        [Required]
        [MinLength(5)]
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
