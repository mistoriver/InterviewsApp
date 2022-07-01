using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Models;
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
        public Response<PositionDto> Get(Guid id, Guid userId);
        /// <summary>
        /// Получить вакансии пользователя
        /// </summary>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        /// <returns>Коллекция вакансий пользователя</returns>
        public Response<IEnumerable<PositionDto>> GetByUserId(Guid userId);
        /// <summary>
        /// Создать вакансию в системе
        /// </summary>
        /// <param name="dto">Параметры вакансии</param>
        /// <returns>Уникальный идентификатор созданной вакансии</returns>
        public Response<Guid> CreatePosition(CreatePositionDto dto);
        /// <summary>
        /// Обновить информацию о зарплатной вилке
        /// </summary>
        /// <param name="dto">Параметры вакансии</param>
        public Response UpdateMoney(UpdatePositionDto dto);
        /// <summary>
        /// Отметить получение отказа по вакансии
        /// </summary>
        /// <param name="id">Уникальный идентификатор вакансии</param>
        public Response UpdateSetDenied(Guid id, Guid userId);
        /// <summary>
        /// Отметить получение оффера по вакансии
        /// </summary>
        /// <param name="id">Уникальный идентификатор вакансии</param>
        public Response UpdateSetOffered(Guid id, Guid userId);

        public Response UpdateComment(UpdateCommentDto dto);
        /// <summary>
        /// Обновить информацию о городе
        /// </summary>
        /// <param name="dto">Параметры вакансии</param>
        public Response UpdateCity(UpdatePositionDto dto);

    }
}
