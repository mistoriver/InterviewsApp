using AutoMapper;
using InterviewsApp.Core.DTOs.External;
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
    public abstract class BaseDbService<TEntity, TExternalDto> : IDbService<TEntity, TExternalDto> 
        where TEntity : BaseEntity 
        where TExternalDto : BaseExternalDto
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public BaseDbService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        public virtual TExternalDto Get(Guid id)
        {
            var entity = _repository.GetByIdOrDefault(id);
            return _mapper.Map<TExternalDto>(entity);
        }

        public virtual IEnumerable<TExternalDto> Get()
        { 
            var entityList = _repository.Get(e => true);
            entityList.Select(entity => _mapper.Map<TExternalDto>(entity));
            return entityList.Select(entity => _mapper.Map<TExternalDto>(entity));
        }

        public virtual void Delete(Guid id)
        {
            var entity = _repository.GetByIdOrDefault(id);
            if (entity != null)
                _repository.Delete(entity);
        }
    }
}
