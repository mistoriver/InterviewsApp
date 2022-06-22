using InterviewsApp.Core.DTOs;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с вакансиями
    /// </summary>
    public interface IPositionService : IDbService<PositionEntity>
    {
        /// <summary>
        /// Создать вакансию в системе
        /// </summary>
        /// <param name="dto">Параметры вакансии</param>
        public void CreatePosition(CreatePositionDto dto);
        /// <summary>
        /// Обновить информацию о зарплатной вилке
        /// </summary>
        /// <param name="dto">Параметры вакансии</param>
        public void UpdateMoney(UpdatePositionDto dto);
        /// <summary>
        /// Отметить получение отказа по вакансии
        /// </summary>
        /// <param name="id">Уникальный идентификатор вакансии</param>
        public void UpdateSetDenied(Guid id);
        /// <summary>
        /// Отметить получение оффера по вакансии
        /// </summary>
        /// <param name="id">Уникальный идентификатор вакансии</param>
        public void UpdateSetOffered(Guid id);

    }
}
