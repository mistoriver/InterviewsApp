using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
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
    public interface IPositionService : IDbService<PositionEntity, PositionDto>
    {
        /// <summary>
        /// Получить конкретную вакансию пользователя с названием компании
        /// </summary>
        /// <param name="id">Уникальный идентификатор вакансии</param>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        /// <returns></returns>
        public PositionDto Get(Guid id, Guid userId);
        /// <summary>
        /// Получить вакансии пользователя
        /// </summary>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        /// <returns>Коллекция вакансий пользователя</returns>
        public IEnumerable<PositionDto> GetByUserId(Guid userId);
        /// <summary>
        /// Создать вакансию в системе
        /// </summary>
        /// <param name="dto">Параметры вакансии</param>
        /// <returns>Уникальный идентификатор созданной вакансии</returns>
        public Guid CreatePosition(CreatePositionDto dto);
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

        public void UpdateComment(UpdateCommentDto dto);

    }
}
