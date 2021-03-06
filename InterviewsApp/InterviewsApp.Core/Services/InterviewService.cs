using AutoMapper;
using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Interfaces;
using InterviewsApp.Core.Models;
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
        public override Response<IEnumerable<InterviewDto>> Get()
        {
            return new Response<IEnumerable<InterviewDto>>("Для получения списка собеседований необходима информация о пользователе");
        }
        public override Response<InterviewDto> Get(Guid id)
        {
            return new Response<InterviewDto>("Для получения собеседования необходима информация о пользователе");
        }
        public Response<InterviewDto> Get(Guid id, Guid userId)
        {
            var interviewDto = GetByUserId(userId).ResponseData.FirstOrDefault(i => i.Id == id);
            if (interviewDto != null)
            {
                return new Response<InterviewDto>(interviewDto);
            }
            return new Response<InterviewDto>("Собеседование не существует");
        }
        public Response<IEnumerable<InterviewDto>> GetByUserId(Guid userId, bool showOnlyFuture = false)
        {
            var positions = _positionRepository.Get(p => p.UserId == userId)?.Select(p => p.Id);
            var ints = _repository.Get(i => positions.Any(p => i.PositionId == p));
            if (showOnlyFuture)
            {
                var dt = DateTime.Now;
                ints = ints.Where(i => i.Date > dt);
            }
            var res = new List<InterviewDto>();
            ints.ToList().ForEach(i => 
            {
                var interview = _mapper.Map<InterviewDto>(i);
                var position = _positionRepository.GetByIdOrDefault(i.PositionId);
                interview.PositionName = position.Name;
                interview.OfferReceived = position.OfferReceived;
                interview.DenialReceived = position.DenialReceived;
                var company = _companyRepository.GetByIdOrDefault(position.CompanyId);
                interview.CompanyName = company.Name;
                interview.CompanyId = company.Id;
                res.Add(interview);
            });
            return new Response<IEnumerable<InterviewDto>>(res);
        }
        public Response<IEnumerable<InterviewDto>> GetByPosition(Guid positionId, Guid userId)
        {
            var interviewDtoList = GetByUserId(userId).ResponseData.Where(i => i.PositionId == positionId);
            if (interviewDtoList.Count() > 0)
            {
                return new Response<IEnumerable<InterviewDto>>(interviewDtoList);
            }
            return new ("Собеседование не существует");
        }

        public Response<Guid> CreateInterview(CreateInterviewDto dto)
        {
            var position = _positionRepository.GetByIdOrDefault(dto.PositionId);
            var interview = _mapper.Map<InterviewEntity>(dto);
            interview.Position = position;
            if (interview.Date.Kind == DateTimeKind.Unspecified)
                interview.Date = new DateTime(dto.Date.Ticks, DateTimeKind.Utc);
            return new Response<Guid>(_repository.Create(interview));
        }
        public Response UpdateComment(UpdateCommentDto dto)
        {
            var interview = GetInterviewOrDefault(dto.Id, dto.UserId);
            if (interview != null)
            {
                interview.Comment = dto.Comment;
                _repository.Update(interview);
                return new Response();
            }
            return new Response("Собеседование, которое вы пытаетесь отредактировать, не существует");
        }
        public Response UpdateDatetime(UpdateInterviewDto dto)
        {
            var interview = GetInterviewOrDefault(dto.Id, dto.UserId); 
            if (interview != null)
            {
                interview.Date = dto.Date;
                _repository.Update(interview);
                return new Response();
            }
            return new Response("Собеседование, которое вы пытаетесь отредактировать, не существует");
        }
        public Response Delete(Guid id, Guid userId)
        {
            return base.Delete(GetInterviewOrDefault(id, userId));
        }
        private InterviewEntity GetInterviewOrDefault(Guid id, Guid userId)
        {
            var positionIds = _positionRepository.Get(p => p.UserId == userId).Select(p => p.Id);
            return _repository.Get(e => e.Id == id && positionIds.Contains(e.PositionId)).FirstOrDefault();
        }
    }
}
