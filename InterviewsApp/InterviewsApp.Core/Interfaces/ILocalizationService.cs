using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Models;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewsApp.Core.Interfaces
{
    public interface ILocalizationService : IDbService<LocalizationEntity, LocalizationDto>
    {
        /// <summary>
        /// Получить список строк по языку
        /// </summary>
        /// <param name="language">Код языка</param>
        /// <returns></returns>
        public Task<Response<IEnumerable<LocalizationDto>>> GetByLanguage(string language);
        /// <summary>
        /// Получить список строк по языку пользователя
        /// </summary>
        /// <param name="userId">Код языка</param>
        /// <returns></returns>
        public Task<Response<IEnumerable<LocalizationDto>>> GetByUserId(Guid userId);
        /// <summary>
        /// Установить локализацию пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="langCode">Код языка</param>
        /// <returns></returns>
        public Task<Response> SetLocalizationForUser(Guid userId, string langCode);
    }
}
