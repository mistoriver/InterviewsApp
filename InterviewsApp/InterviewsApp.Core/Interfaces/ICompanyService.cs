using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Data.Models.Entities;
using System;

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
        public Guid CreateCompany(CreateCompanyDto dto);
        /// <summary>
        /// Оценить компанию
        /// </summary>
        /// <param name="id">Уникальный идентификатор компании</param>
        /// <param name="newRate">Новая оценка пользователя</param>
        /// <returns>Новое значение рейтинга</returns>
        public short RateCompany(Guid id, short newRate);
    }
}
