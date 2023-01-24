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
    /// Общий интерфейс для сервиса работы с БД
    /// </summary>
    /// <typeparam name="TEntity">Сущность БД</typeparam>
    public interface IDbService<TEntity, TExternalDto> 
        //TODO: Возможно, заменить прямую привязку к классам на интерфейсы
        where TEntity : BaseEntity
        where TExternalDto : BaseExternalDto
    {
        /// <summary>
        /// Получить данные конкретной сущности
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Данные сущности</returns>
        public Task<Response<TExternalDto>> Get(Guid id);
        /// <summary>
        /// Получить все сущности в системе
        /// </summary>
        /// <returns>Список всех сущностей</returns>
        public Task<Response<IEnumerable<TExternalDto>>> Get();
    }
}
