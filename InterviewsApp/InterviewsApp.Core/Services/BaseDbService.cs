using AutoMapper;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Interfaces;
using InterviewsApp.Core.Models;
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
        public virtual async Task<Response<TExternalDto>> Get(Guid id)
        {
            var entity = await _repository.GetByIdOrDefault(id);
            return new Response<TExternalDto>(_mapper.Map<TExternalDto>(entity));
        }

        public virtual async Task<Response<IEnumerable<TExternalDto>>> Get()
        { 
            var entityList = await _repository.Get(e => true);
            entityList.Select(entity => _mapper.Map<TExternalDto>(entity));
            return new Response<IEnumerable<TExternalDto>>(entityList.Select(entity => _mapper.Map<TExternalDto>(entity)));
        }

        protected async Task<Response> Delete(TEntity entity)
        {
            if (entity != null)
            {
                await _repository.Delete(entity);
                return new Response();
            }
            return new Response("Loc.Message.NoSuchForDelete");
        }
    }
}
