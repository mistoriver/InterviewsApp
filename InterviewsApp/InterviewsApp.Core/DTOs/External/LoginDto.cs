using System;

namespace InterviewsApp.WebAPI.Models
{
    /// <summary>
    /// Транспортный объект для возвращения ID пользователя и JWT-токена аутентифицированному пользователю
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// JWT-токен
        /// </summary>
        public string Token { get; set; } 
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; } 
    }
}
