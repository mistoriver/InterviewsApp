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
    public class PositionService : BaseDbService<PositionEntity, PositionDto>, IPositionService
    {
        protected readonly IRepository<UserEntity> _userRepository;
        protected readonly IRepository<CompanyEntity> _companyRepository;
        public PositionService(IRepository<PositionEntity> repository, IRepository<UserEntity> userRepository, IRepository<CompanyEntity> companyRepository, IMapper mapper) : base(repository, mapper)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
        }
        public async Task<Response<PositionDto>> Get(Guid id, Guid userId)
        {
            var pos = (await _repository.Get(e => e.Id == id && e.UserId == userId)).FirstOrDefault();
            if (pos != null)
            {
                var posDto = _mapper.Map<PositionDto>(pos);
                posDto.CompanyName = (await _companyRepository.GetByIdOrDefault(posDto.CompanyId))?.Name;
                return new Response<PositionDto>(posDto);
            }
            return new Response<PositionDto>("Вакансия не существует");
        }
        public async Task<Response<IEnumerable<PositionDto>>> GetByUserId(Guid userId)
        {
            var positions = (await _repository.Get(p => p.UserId == userId)).Select(p => _mapper.Map<PositionDto>(p));
            var res = positions.ToList();
            foreach (var position in res)
            {
                position.CompanyName = (await _companyRepository.GetByIdOrDefault(position.CompanyId))?.Name;
            }
            return new Response<IEnumerable<PositionDto>>(res);
        }
        public async Task<Response<Guid>> CreatePosition(CreatePositionDto dto)
        {
            var user = await _userRepository.GetByIdOrDefault(dto.UserId);
            var company = await _companyRepository.GetByIdOrDefault(dto.CompanyId);
            var position = _mapper.Map<PositionEntity>(dto);
            if (user != null)
            {
                if (company != null)
                {
                    position.MoneyLower = 0;
                    position.MoneyUpper = 0;
                    position.User = user;
                    position.Company = company;
                    return new Response<Guid>(await _repository.Create(position));
                }
                return new Response<Guid>("Компания не существует");
            }
            return new Response<Guid>("Пользователь не существует");

        }
        public async Task<Response> UpdateMoney(UpdatePositionDto dto)
        {
            var position = (await _repository.Get(e => e.Id == dto.Id && e.UserId == dto.UserId)).FirstOrDefault();
            if (position != null)
            {
                position.MoneyLower = dto.MoneyLower;
                position.MoneyUpper = dto.MoneyUpper;
                await _repository.Update(position);
                return new Response();
            }
            return new Response("Вакансия, которую вы пытаетесь обновить, не существует");
        }
        public async Task<Response> UpdateSetDenied(Guid id, Guid userId)
        {
            var position = (await _repository.Get(e => e.Id == id && e.UserId == userId)).FirstOrDefault();
            if (position != null)
            {
                position.DenialReceived = true;
                position.OfferReceived = false;
                await _repository.Update(position);
                return new Response();
            }
            return new Response("Вакансия, которую вы пытаетесь обновить, не существует");
        }
        public async Task<Response> UpdateSetOffered(Guid id, Guid userId)
        {
            var position = (await _repository.Get(e => e.Id == id && e.UserId == userId)).FirstOrDefault();
            if (position != null)
            {
                position.OfferReceived = true;
                position.DenialReceived = false;
                await _repository.Update(position);
                return new Response();
            }
            return new Response("Вакансия, которую вы пытаетесь обновить, не существует");
        }
        public async Task<Response> UpdateComment(UpdateCommentDto dto)
        {
            var position = (await _repository.Get(e => e.Id == dto.Id && e.UserId == dto.UserId)).FirstOrDefault();
            if (position != null)
            {
                position.Comment = dto.Comment;
                await _repository.Update(position);
                return new Response();
            }
            return new Response("Вакансия, которую вы пытаетесь обновить, не существует");
        }
        public async Task<Response> UpdateCity(UpdatePositionDto dto)
        {
            var position = (await _repository.Get(e => e.Id == dto.Id && e.UserId == dto.UserId)).FirstOrDefault();
            if (position != null)
            {
                position.City = dto.City;
                await _repository.Update(position);
                return new Response();
            }
            return new Response("Вакансия, которую вы пытаетесь обновить, не существует");
        }
        public async Task<Response> Delete(Guid id, Guid userId)
        {
            var pos = (await _repository.Get(e => e.Id == id && e.UserId == userId)).FirstOrDefault();
            return await base.Delete(pos);
        }
    }
}
