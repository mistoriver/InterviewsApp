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
        private readonly IRepository<CompanyEntity> _companyRepository;

        public InterviewService(IRepository<InterviewEntity> repository, IRepository<PositionEntity> positionRepository, IRepository<CompanyEntity> companyRepository, IMapper mapper) :base(repository, mapper)
        {
            _positionRepository = positionRepository;
            _companyRepository = companyRepository;
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
        public IEnumerable<InterviewUiDto> GetByUserIdForUi(Guid userId)
        {
            var dtoList = GetByUserId(userId);
            var res = new List<InterviewUiDto>();
            dtoList.ToList().ForEach(dto =>
            {
                var _uiDto = _mapper.Map<InterviewUiDto>(dto);
                var position = _positionRepository.GetByIdOrDefault(dto.PositionId);
                _uiDto.PositionName = position.Name;
                _uiDto.CompanyName = _companyRepository.GetByIdOrDefault(position.CompanyId).Name;
                res.Add(_uiDto);
            });
            return res;
        }

        public void CreateInterview(CreateInterviewDto dto)
        {
            var position = _positionRepository.GetByIdOrDefault(dto.PositionId);
            var interview = _mapper.Map<InterviewEntity>(dto);
            interview.Position = position;
            if (interview.Date.Kind == DateTimeKind.Unspecified)
                interview.Date = new DateTime(dto.Date.Ticks, DateTimeKind.Utc);
            _repository.Create(interview);
        }
    }
}
