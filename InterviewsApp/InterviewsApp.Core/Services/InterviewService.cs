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
        public override InterviewDto Get(Guid id)
        {
            return new InterviewDto();
        }
        public InterviewDto Get(Guid id, Guid userId)
        {
            return GetByUserId(userId)?.FirstOrDefault(i => i.Id == id);
        }
        public IEnumerable<InterviewDto> GetByUserId(Guid userId)
        {
            var positions = _positionRepository.Get(p => p.UserId == userId)?.Select(p => p.Id);
            var ints = _repository.Get(i => positions.Any(p => i.PositionId == p));
            var res = new List<InterviewDto>();
            ints.ToList().ForEach(i => 
            {
                var interview = _mapper.Map<InterviewDto>(i);
                var position = _positionRepository.GetByIdOrDefault(i.PositionId);
                interview.PositionName = position.Name;
                var company = _companyRepository.GetByIdOrDefault(position.CompanyId);
                interview.CompanyName = company.Name;
                interview.CompanyId = company.Id;
                res.Add(interview);
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
        public void UpdateComment(UpdateCommentDto dto)
        {
            var positionIds = _positionRepository.Get(p => p.UserId == dto.UserId).Select(p => p.Id);
            var interview = _repository.Get(e => e.Id == dto.Id && positionIds.Contains(e.PositionId)).FirstOrDefault();
            if (interview != null)
            {
                interview.Comment = dto.Comment;
                _repository.Update(interview);
            }
        }
        public void UpdateDatetime(UpdateInterviewDto dto)
        {
            var positionIds = _positionRepository.Get(p => p.UserId == dto.UserId).Select(p => p.Id);
            var interview = _repository.Get(e => e.Id == dto.Id && positionIds.Contains(e.PositionId)).FirstOrDefault();
            if (interview != null)
            {
                interview.Date = dto.Date;
                _repository.Update(interview);
            }
        }
    }
}
