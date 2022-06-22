using InterviewsApp.Core.DTOs;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;

namespace InterviewsApp.Core.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса работы с собеседованиями
    /// </summary>
    public interface IInterviewService : IDbService<InterviewEntity>
    {
        /// <summary>
        /// Получить все собеседования
        /// </summary>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        /// <returns>Коллекция собеседований пользователя</returns>
        public IEnumerable<InterviewEntity> GetByUserId(Guid userId);
        /// <summary>
        /// Создать собеседование в системе
        /// </summary>
        /// <param name="dto">Данные для создания собеседование</param>
        public void CreateInterview(CreateInterviewDto dto);
    }
}
