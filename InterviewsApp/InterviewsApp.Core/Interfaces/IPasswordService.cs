using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.Interfaces
{
    /// <summary>
    /// Интерфейс для сервиса работы с паролями
    /// </summary>
    public interface IPasswordService
    {
        /// <summary>
        /// Получить хеш пароля
        /// </summary>
        /// <param name="password">Пароль, который нужно захешировать</param>
        /// <returns></returns>
        public string HashPassword(string password);
        /// <summary>
        /// Сверить пароль с хешем
        /// </summary>
        /// <param name="password">Пароль в исхоном виде</param>
        /// <param name="passHash">Потенциальный хеш</param>
        /// <returns></returns>
        public bool VerifyPassword(string password, string passHash);
    }
}
