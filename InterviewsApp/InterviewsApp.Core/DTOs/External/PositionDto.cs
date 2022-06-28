using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.DTOs.External
{
    public class PositionDto :BaseExternalDto
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Нижняя вилка
        /// </summary>
        public int MoneyLower { get; set; }

        /// <summary>
        /// Верхняя вилка
        /// </summary>
        public int MoneyUpper { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Путь к комментарию
        /// </summary>
        public string PathToComment { get; set; }

        /// <summary>
        /// Флаг получения оффера
        /// </summary>
        public bool OfferReceived { get; set; }

        /// <summary>
        /// Флаг получения отказа
        /// </summary>
        public bool DenialReceived { get; set; }
        /// <summary>
        /// Название компании
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Уникальный идентификатор компании
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }
    }
}
