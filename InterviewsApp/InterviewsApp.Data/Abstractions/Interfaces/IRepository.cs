using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InterviewsApp.Data.Abstractions.Interfaces
{
    /// <summary>
    /// Резопиторий для доступа к данным в бд
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Получить экземпляр сущности по уникальному идентификатору
        /// </summary>
        /// <param name="entityId">Уникальный идентификатор</param>
        /// <returns>Экземпляр сущности типа <see cref="TEntity"/></returns>
        Task<TEntity?> GetByIdOrDefault(Guid entityId);

        /// <summary>
        /// Возвращает коллекцию экземпляров сущности, соответствующую условию предиката
        /// </summary>
        /// <param name="predicate">Предикат, содержащий условие для отбора</param>
        /// <returns>Коллекция экземпляров сущности <see cref="TEntity"/></returns>
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Обновить данные сущности
        /// </summary>
        /// <param name="entity">Экземпляр сущности для обновления</param>
        Task Update(TEntity entity);
        /// <summary>
        /// Обновить данные набора сущностей
        /// </summary>
        /// <param name="entities">Набор сущностей</param>
        Task UpdateRange(params TEntity[] entities);

        /// <summary>
        /// Создать новый экземпляр сущности в бд
        /// </summary>
        /// <param name="entity">Экземпляр сущности</param>
        Task<Guid> Create(TEntity entity);

        /// <summary>
        /// Удалить сущность из бд
        /// </summary>
        /// <param name="entity">Экземпляр сущности</param>
        Task Delete(TEntity entity);
    }
}
