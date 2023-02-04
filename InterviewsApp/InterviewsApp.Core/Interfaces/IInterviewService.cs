using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Models;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public Task<Response<InterviewDto>> Get(Guid id, Guid userId);
        /// <summary>
        /// Получить собеседования пользователя
        /// </summary>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        /// <returns>Коллекция собеседований пользователя</returns>
        public Task<Response<IEnumerable<InterviewDto>>> GetByUserId(Guid userId, bool showOnlyFuture);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<Response<IEnumerable<InterviewDto>>> GetByPosition(Guid positionId, Guid userId);
        /// <summary>
        /// Создать собеседование в системе
        /// </summary>
        /// <param name="dto">Данные для создания собеседование</param>
        public Task<Response<Guid>> CreateInterview(CreateInterviewDto dto);

        public Task<Response> UpdateComment(UpdateCommentDto dto);
        /// <summary>
        /// Обновить дату и время собеседования
        /// </summary>
        /// <param name="dto">Данные для обновления</param>
        public Task<Response> UpdateDatetime(UpdateInterviewDto dto);


        /// <summary>
        /// Удалить собеседование из системы
        /// </summary>
        /// <param name="id">Уникальный идентификатор собеседования</param>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        public Task<Response> Delete(Guid id, Guid userId);
    }
}
