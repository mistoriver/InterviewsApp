namespace InterviewsApp.Core.Models
{
    /// <summary>
    /// Настройки аутентификации
    /// </summary>
    public class AuthSettings
    {
        /// <summary>
        /// Секретный ключ
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Количество дней, в течение которого jwt-токен будет валиден
        /// </summary>
        public int LifeTimeHours { get; set; }
    }
}
