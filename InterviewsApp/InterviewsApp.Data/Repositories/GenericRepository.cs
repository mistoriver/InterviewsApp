using InterviewsApp.Data.Abstractions;
using InterviewsApp.Data.Models.Entities;

namespace InterviewsApp.Data.Repositories
{
    /// <summary>
    /// Универсальный репозиторий
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> : BaseRepository<TEntity> where TEntity : BaseEntity
    {
        public GenericRepository(InterviewsContext context) : base(context)
        { }
    }
}
