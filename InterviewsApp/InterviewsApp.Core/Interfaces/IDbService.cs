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
    public interface IDbService<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Получить данные конкретной сущности
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Данные сущности</returns>
        public TEntity Get(Guid id);
        /// <summary>
        /// Получить все сущности в системе
        /// </summary>
        /// <returns>Список всех сущностей</returns>
        public IEnumerable<TEntity> Get();

        /// <summary>
        /// Удалить сущность из системы
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        public void Delete(Guid id);
    }
}
