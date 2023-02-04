using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Models;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Threading.Tasks;

namespace InterviewsApp.Core.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с компаниями
    /// </summary>
    public interface ICompanyService : IDbService<CompanyEntity, CompanyDto>
    {
        /// <summary>
        /// Создать компанию в системе
        /// </summary>
        /// <param name="dto">Параметры компании</param>
        /// <returns>Уникальный идентификатор созданной компании</returns>
        public Task<Response<Guid>> CreateCompany(CreateCompanyDto dto);
        /// <summary>
        /// Оценить компанию
        /// </summary>
        /// <param name="id">Уникальный идентификатор компании</param>
        /// <param name="newRate">Новая оценка пользователя</param>
        /// <returns>Новое значение рейтинга</returns>
        public Task<Response<short>> RateCompany(Guid id, Guid userId, short newRate);
        /// <summary>
        /// Получить оценку, выставленную компании конкретным пользователем
        /// </summary>
        /// <param name="id">Уникальный идентификатор компании</param>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        /// <returns>Выставленная оценка</returns>
        public Task<Response<short>> GetUserCompanyRate(Guid id, Guid userId);
    }
}
