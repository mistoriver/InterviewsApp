using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
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
        /// Получить собеседования пользователя
        /// </summary>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        /// <returns>Коллекция собеседований пользователя</returns>
        public IEnumerable<InterviewDto> GetByUserId(Guid userId);
        /// <summary>
        /// Получить собеседования пользователя c названиями вакансий и компаний
        /// </summary>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        /// <returns>Коллекция собеседований пользователя c названиями вакансий и компаний</returns>
        public IEnumerable<InterviewUiDto> GetByUserIdForUi(Guid userId);
        /// <summary>
        /// Создать собеседование в системе
        /// </summary>
        /// <param name="dto">Данные для создания собеседование</param>
        public void CreateInterview(CreateInterviewDto dto);
    }
}
