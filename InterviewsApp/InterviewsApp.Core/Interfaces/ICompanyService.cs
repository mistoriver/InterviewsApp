using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с компаниями
    /// </summary>
    public interface ICompanyService : IDbService<CompanyEntity>
    {
        /// <summary>
        /// Создать компанию в системе
        /// </summary>
        /// <param name="name">Название компании</param>
        public void CreateCompany(string name);
        /// <summary>
        /// Оценить компанию
        /// </summary>
        /// <param name="id">Уникальный идентификатор компании</param>
        /// <param name="newRate">Новая оценка пользователя</param>
        public void RateCompany(Guid id, short newRate);
    }
}
