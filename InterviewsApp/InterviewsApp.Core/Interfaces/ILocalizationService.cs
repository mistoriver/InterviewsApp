using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Models;
using InterviewsApp.Data.Models.Entities;
using InterviewsApp.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
