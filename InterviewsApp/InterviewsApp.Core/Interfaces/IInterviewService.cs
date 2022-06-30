using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Models;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;

namespace InterviewsApp.Core.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса работы с собеседованиями
    /// </summary>
    public interface IInterviewService : IDbService<InterviewEntity, InterviewDto>
    {
        /// <summary>
        /// Получить конкретное собеседование пользователя
        /// </summary>
        /// <param name="id">Уникальный идентификатор собеседования</param>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        /// <returns></returns>
        public Response<InterviewDto> Get(Guid id, Guid userId);
        /// <summary>
        /// Получить собеседования пользователя
        /// </summary>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        /// <returns>Коллекция собеседований пользователя</returns>
        public Response<IEnumerable<InterviewDto>> GetByUserId(Guid userId);
        /// <summary>
        /// Создать собеседование в системе
        /// </summary>
        /// <param name="dto">Данные для создания собеседование</param>
        public Response<Guid> CreateInterview(CreateInterviewDto dto);

        public Response UpdateComment(UpdateCommentDto dto);
        /// <summary>
        /// Обновить дату и время собеседования
        /// </summary>
        /// <param name="dto">Данные для обновления</param>
        public Response UpdateDatetime(UpdateInterviewDto dto);
    }
}
