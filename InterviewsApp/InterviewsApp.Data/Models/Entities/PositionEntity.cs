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
        public string Name { get; set; } = null!;

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
        public string City { get; set; } = null!;

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Флаг получения оффера
        /// </summary>
        public bool OfferReceived { get; set; }

        /// <summary>
        /// Флаг получения отказа
        /// </summary>
        public bool DenialReceived { get; set; }

        /// <summary>
        /// Оценка, выставленная родительской компании
        /// </summary>
        public short CompanyRate { get; set; }

        /// <summary>
        /// Уникальный идентификатор компании
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Навигационное свойство для связи с сущностью <see cref="CompanyEntity"/>
        /// </summary>
        public CompanyEntity Company { get; set; } = new();

        /// <summary>
        /// Навигационное свойство для связи с сущностью <see cref="UserEntity"/>
        /// </summary>
        public UserEntity User { get; set; } = new();

        /// <summary>
        /// Навигационное свойство для связи с сущностью <see cref="Interviews"/>
        /// </summary>
        public List<InterviewEntity> Interviews { get; set; } = new();
    }
}
