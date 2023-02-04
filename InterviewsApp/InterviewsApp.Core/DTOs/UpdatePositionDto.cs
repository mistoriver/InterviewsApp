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
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Loc.Message.PositionNameSize")]
        public string Name { get; set; }

        /// <summary>
        /// Нижняя вилка
        /// </summary>
        [Range(0, 10000000, ErrorMessage = "Loc.Message.MoneyLower")]
        public int MoneyLower { get; set; }

        /// <summary>
        /// Верхняя вилка
        /// </summary>
        [Range(0, 10000000, ErrorMessage = "Loc.Message.MoneyUpper")]
        public int MoneyUpper { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Loc.Message.PositionCitySize")]
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
