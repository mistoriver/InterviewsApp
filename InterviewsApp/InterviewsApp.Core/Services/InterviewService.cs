using AutoMapper;
using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Interfaces;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewsApp.Core.Services
{
    public class InterviewService : BaseDbService<InterviewEntity, InterviewDto>, IInterviewService
    {
        private readonly IRepository<PositionEntity> _positionRepository;

        public InterviewService(IRepository<InterviewEntity> repository, IRepository<PositionEntity> positionRepository, IMapper mapper) :base(repository, mapper)
        {
            _positionRepository = positionRepository;
        }
        public override IEnumerable<InterviewDto> Get()
        {
            return new List<InterviewDto>();
        }
        public IEnumerable<InterviewDto> GetByUserId(Guid userId)
        {
            var positions = _positionRepository.Get(p => p.UserId == userId)?.Select(p => p.Id);
            var ints = _repository.Get(i => positions.Any(p => i.PositionId == p));
            var res = new List<InterviewDto>();
            ints.ToList().ForEach(i => res.Add(_mapper.Map<InterviewDto>(i)));
            return res;
        }

        public void CreateInterview(CreateInterviewDto dto)
        {
            var position = _positionRepository.GetByIdOrDefault(dto.PositionId);
            var interview = _mapper.Map<InterviewEntity>(dto);
            interview.Position = position;
            _repository.Create(interview);
        }
    }
}
