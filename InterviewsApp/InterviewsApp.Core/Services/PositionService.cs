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
    public class PositionService : BaseDbService<PositionEntity, PositionDto>, IPositionService
    {
        protected readonly IRepository<UserEntity> _userRepository;
        protected readonly IRepository<CompanyEntity> _companyRepository;
        public PositionService(IRepository<PositionEntity> repository, IRepository<UserEntity> userRepository, IRepository<CompanyEntity> companyRepository, IMapper mapper) : base(repository, mapper)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
        }
        public Response<PositionDto> Get(Guid id, Guid userId)
        {
            var pos = _repository.Get(e => e.Id == id && e.UserId == userId).FirstOrDefault();
            if (pos != null)
            {
                var posDto = _mapper.Map<PositionDto>(pos);
                posDto.CompanyName = _companyRepository.GetByIdOrDefault(posDto.CompanyId)?.Name;
                return new Response<PositionDto>(posDto);
            }
            return new Response<PositionDto>("Вакансия не существует");
        }
        public Response<IEnumerable<PositionDto>> GetByUserId(Guid userId)
        {
            var positions = _repository.Get(p => p.UserId == userId).Select(p => _mapper.Map<PositionDto>(p));
            var res = positions.ToList();
            res.ForEach(p => p.CompanyName = _companyRepository.GetByIdOrDefault(p.CompanyId).Name);
            return new Response<IEnumerable<PositionDto>>(res);
        }
        public Response<Guid> CreatePosition(CreatePositionDto dto)
        {
            var user = _userRepository.GetByIdOrDefault(dto.UserId);
            var company = _companyRepository.GetByIdOrDefault(dto.CompanyId);
            var position = _mapper.Map<PositionEntity>(dto);
            position.MoneyLower = 0;
            position.MoneyUpper = 0;
            position.User = user;
            position.Company = company;
            return new Response<Guid>(_repository.Create(position));
        }
        public Response UpdateMoney(UpdatePositionDto dto)
        {
            var position = _repository.Get(e => e.Id == dto.Id && e.UserId == dto.UserId).FirstOrDefault();
            if (position != null)
            {
                position.MoneyLower = dto.MoneyLower;
                position.MoneyUpper = dto.MoneyUpper;
                _repository.Update(position);
                return new Response();
            }
            return new Response("Вакансия, которую вы пытаетесь обновить, не существует");
        }
        public Response UpdateSetDenied(Guid id, Guid userId)
        {
            var position = _repository.Get(e => e.Id == id && e.UserId == userId).FirstOrDefault();
            if (position != null)
            {
                position.DenialReceived = true;
                position.OfferReceived = false;
                _repository.Update(position);
                return new Response();
            }
            return new Response("Вакансия, которую вы пытаетесь обновить, не существует");
        }
        public Response UpdateSetOffered(Guid id, Guid userId)
        {
            var position = _repository.Get(e => e.Id == id && e.UserId == userId).FirstOrDefault();
            if (position != null)
            {
                position.OfferReceived = true;
                position.DenialReceived = false;
                _repository.Update(position);
                return new Response();
            }
            return new Response("Вакансия, которую вы пытаетесь обновить, не существует");
        }
        public Response UpdateComment(UpdateCommentDto dto)
        {
            var position = _repository.Get(e => e.Id == dto.Id && e.UserId == dto.UserId).FirstOrDefault();
            if (position != null)
            {
                position.Comment = dto.Comment;
                _repository.Update(position);
                return new Response();
            }
            return new Response("Вакансия, которую вы пытаетесь обновить, не существует");
        }
        public Response UpdateCity(UpdatePositionDto dto)
        {
            var position = _repository.Get(e => e.Id == dto.Id && e.UserId == dto.UserId).FirstOrDefault();
            if (position != null)
            {
                position.City = dto.City;
                _repository.Update(position);
                return new Response();
            }
            return new Response("Вакансия, которую вы пытаетесь обновить, не существует");
        }
        public Response Delete(Guid id, Guid userId)
        {
            var pos = _repository.Get(e => e.Id == id && e.UserId == userId).FirstOrDefault();
            return base.Delete(pos);
        }
    }
}
