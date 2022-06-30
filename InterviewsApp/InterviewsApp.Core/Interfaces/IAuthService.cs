using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Сгенерировать токен авторизации
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Токен авторизации</returns>
        string Generate(string login);
    }
}
