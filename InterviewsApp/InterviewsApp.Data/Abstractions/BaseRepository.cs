using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace InterviewsApp.Data.Abstractions
{
    /// <summary>
    /// Базовый репозиторий
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected InterviewsContext AppContext { get; }

        protected BaseRepository(InterviewsContext context)
        {
            AppContext = context;
        }

        public TEntity? GetByIdOrDefault(Guid entityId)
        {
            return AppContext.Set<TEntity>().Find(entityId);
        }

        public IEnumerable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> predicate)
        {
            return AppContext.Set<TEntity>().Where(predicate).ToList();
        }

        public void Update(TEntity entity)
        {
            AppContext.Set<TEntity>().Update(entity);
            AppContext.SaveChanges();
        }

        public void Create(TEntity entity)
        {
            AppContext.Add(entity);
            AppContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            AppContext.Remove(entity);
            AppContext.SaveChanges();
        }
    }
}
