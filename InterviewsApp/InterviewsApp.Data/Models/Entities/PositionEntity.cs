using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Data.Models.Entities
{
    /// <summary>
    /// Вакансия
    /// </summary>
    public class PositionEntity : BaseEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; init; } = null!;

        /// <summary>
        /// Нижняя вилка
        /// </summary>
        public int MoneyLower { get; init; }

        /// <summary>
        /// Верхняя вилка
        /// </summary>
        public int MoneyUpper { get; init; }

        /// <summary>
        /// Город
        /// </summary>
        public string City { get; init; } = null!;

        /// <summary>
        /// Путь к комментарию
        /// </summary>
        public string? PathToComment { get; init; }

        /// <summary>
        /// Флаг получения оффера
        /// </summary>
        public bool OfferReceived { get; init; }

        /// <summary>
        /// Флаг получения отказа
        /// </summary>
        public bool DenialReceived { get; init; }

        /// <summary>
        /// Уникальный идентификатор компании
        /// </summary>
        public Guid CompanyId { get; init; }

        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public Guid UserId { get; init; }

        /// <summary>
        /// Навигационное свойство для связи с сущностью <see cref="CompanyEntity"/>
        /// </summary>
        public CompanyEntity Company { get; init; } = new();

        /// <summary>
        /// Навигационное свойство для связи с сущностью <see cref="UserEntity"/>
        /// </summary>
        public UserEntity User { get; init; } = new();

        /// <summary>
        /// Навигационное свойство для связи с сущностью <see cref="Interviews"/>
        /// </summary>
        public List<InterviewEntity> Interviews { get; init; } = new();
    }
}
