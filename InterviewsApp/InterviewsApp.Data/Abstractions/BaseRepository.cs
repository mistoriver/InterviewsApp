using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public async Task<TEntity?> GetByIdOrDefault(Guid entityId)
        {
            return await AppContext.Set<TEntity>().FindAsync(entityId);
        }

        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return await AppContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task Update(TEntity entity)
        {
            AppContext.Set<TEntity>().Update(entity);
            await AppContext.SaveChangesAsync();
        }
        public async Task UpdateRange(params TEntity[] entities)
        {
            AppContext.Set<TEntity>().UpdateRange(entities);
            await AppContext.SaveChangesAsync();
        }

        public async Task<Guid> Create(TEntity entity)
        {
            AppContext.Add(entity);
            await AppContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(TEntity entity)
        {
            AppContext.Remove(entity);
            await AppContext.SaveChangesAsync();
        }
        public async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return await AppContext.Set<TEntity>().AnyAsync(predicate);
        }
        [Obsolete]
        public void SaveChanges()
        {
            AppContext.SaveChanges();
        }
    }
}
