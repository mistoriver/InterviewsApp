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
using System.Threading.Tasks;

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
        public override async Task<Response<IEnumerable<InterviewDto>>> Get()
        {
            return new Response<IEnumerable<InterviewDto>>("Для получения списка собеседований необходима информация о пользователе");
        }
        public override async Task<Response<InterviewDto>> Get(Guid id)
        {
            return new Response<InterviewDto>("Для получения собеседования необходима информация о пользователе");
        }
        public async Task<Response<InterviewDto>> Get(Guid id, Guid userId)
        {
            var interviewDto = (await GetByUserId(userId)).ResponseData.FirstOrDefault(i => i.Id == id);
            if (interviewDto != null)
            {
                return new Response<InterviewDto>(interviewDto);
            }
            return new Response<InterviewDto>("Собеседование не существует");
        }
        public async Task<Response<IEnumerable<InterviewDto>>> GetByUserId(Guid userId, bool showOnlyFuture = false)
        {
            var positions = (await _positionRepository.Get(p => p.UserId == userId))?.Select(p => p.Id);
            var ints = await _repository.Get(i => positions.Any(p => i.PositionId == p));
            if (showOnlyFuture)
            {
                var dt = DateTime.Now;
                ints = ints.Where(i => i.Date > dt);
            }
            var res = new List<InterviewDto>();
            ints.ToList().ForEach(async i => 
            {
                var interview = _mapper.Map<InterviewDto>(i);
                var position = await _positionRepository.GetByIdOrDefault(i.PositionId);
                interview.PositionName = position.Name;
                interview.OfferReceived = position.OfferReceived;
                interview.DenialReceived = position.DenialReceived;
                var company = await _companyRepository.GetByIdOrDefault(position.CompanyId);
                interview.CompanyName = company.Name;
                interview.CompanyId = company.Id;
                res.Add(interview);
            });
            return new Response<IEnumerable<InterviewDto>>(res);
        }
        public async Task<Response<IEnumerable<InterviewDto>>> GetByPosition(Guid positionId, Guid userId)
        {
            var interviewDtoList = (await GetByUserId(userId)).ResponseData.Where(i => i.PositionId == positionId);
            if (interviewDtoList.Count() > 0)
            {
                return new Response<IEnumerable<InterviewDto>>(interviewDtoList);
            }
            return new ("Собеседование не существует");
        }

        public async Task<Response<Guid>> CreateInterview(CreateInterviewDto dto)
        {
            var position = await _positionRepository.GetByIdOrDefault(dto.PositionId);
            var interview = _mapper.Map<InterviewEntity>(dto);
            if (position == null)
                return new ("Позиции не существует");
            interview.Position = position;
            if (interview.Date.Kind == DateTimeKind.Unspecified)
                interview.Date = new DateTime(dto.Date.Ticks, DateTimeKind.Utc);
            return new Response<Guid>(await _repository.Create(interview));
        }
        public async Task<Response> UpdateComment(UpdateCommentDto dto)
        {
            var interview = await GetInterviewOrDefault(dto.Id, dto.UserId);
            if (interview != null)
            {
                interview.Comment = dto.Comment;
                await _repository.Update(interview);
                return new Response();
            }
            return new Response("Собеседование, которое вы пытаетесь отредактировать, не существует");
        }
        public async Task<Response> UpdateDatetime(UpdateInterviewDto dto)
        {
            var interview = await GetInterviewOrDefault(dto.Id, dto.UserId); 
            if (interview != null)
            {
                interview.Date = dto.Date;
                await _repository.Update(interview);
                return new Response();
            }
            return new Response("Собеседование, которое вы пытаетесь отредактировать, не существует");
        }
        public async Task<Response> Delete(Guid id, Guid userId)
        {
            return await base.Delete(await GetInterviewOrDefault(id, userId));
        }
        private async Task<InterviewEntity> GetInterviewOrDefault(Guid id, Guid userId)
        {
            var positionIds = (await _positionRepository.Get(p => p.UserId == userId)).Select(p => p.Id);
            return (await _repository.Get(e => e.Id == id && positionIds.Contains(e.PositionId))).FirstOrDefault();
        }
    }
}
