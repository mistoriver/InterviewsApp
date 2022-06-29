using System;
using System.ComponentModel.DataAnnotations;

namespace InterviewsApp.Core.DTOs
{
    public class UpdatePositionDto
    {
        /// <summary>
        /// Уникальный идентификатор сущности
        /// </summary>
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        [Required]
        public Guid UserId { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина имени должна быть в пределах от 3 до 50 символов")]
        public string Name { get; set; }

        /// <summary>
        /// Нижняя вилка
        /// </summary>
        [Range(0, 10000000, ErrorMessage = "Нижняя граница зарплатной вилки должна быть в пределах от 0 до 10 миллионов.")]
        public int MoneyLower { get; set; }

        /// <summary>
        /// Верхняя вилка
        /// </summary>
        [Range(0, 10000000, ErrorMessage = "Верхняя граница зарплатной вилки должна быть в пределах от 0 до 10 миллионов.")]
        public int MoneyUpper { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Длина названия города должна быть в пределах от 1 до 50 символов")]
        public string City { get; set; }

        /// <summary>
        /// Флаг получения оффера
        /// </summary>
        public bool OfferReceived { get; set; }

        /// <summary>
        /// Флаг получения отказа
        /// </summary>
        public bool DenialReceived { get; set; }
    }
}
