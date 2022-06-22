using InterviewsApp.Core.Interfaces;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.Services
{
    public abstract class BaseDbService<TEntity> : IDbService<TEntity> where TEntity : BaseEntity
    {
        protected readonly IRepository<TEntity> _repository;

        public BaseDbService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }
        public virtual TEntity Get(Guid id)
        {
            return _repository.GetByIdOrDefault(id);
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return _repository.Get(e => true);
        }

        public virtual void Delete(Guid id)
        {
            var entity = _repository.GetByIdOrDefault(id);
            if (entity != null)
                _repository.Delete(entity);
        }
    }
}
