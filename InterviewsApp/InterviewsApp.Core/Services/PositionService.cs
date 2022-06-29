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
    public class PositionService : BaseDbService<PositionEntity, PositionDto>, IPositionService
    {
        protected readonly IRepository<UserEntity> _userRepository;
        protected readonly IRepository<CompanyEntity> _companyRepository;
        public PositionService(IRepository<PositionEntity> repository, IRepository<UserEntity> userRepository, IRepository<CompanyEntity> companyRepository, IMapper mapper) : base(repository, mapper)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
        }
        public PositionDto Get(Guid id, Guid userId)
        {
            return GetByUserId(userId)?.FirstOrDefault(i => i.Id == id);
        }
        public IEnumerable<PositionDto> GetByUserId(Guid userId)
        {
            var positions = _repository.Get(p => p.UserId == userId).Select(p => _mapper.Map<PositionDto>(p));
            var res = positions.ToList();
            res.ForEach(p => p.CompanyName = _companyRepository.GetByIdOrDefault(p.CompanyId).Name);
            return res;
        }
        public Guid CreatePosition(CreatePositionDto dto)
        {
            var user = _userRepository.GetByIdOrDefault(dto.UserId);
            var company = _companyRepository.GetByIdOrDefault(dto.CompanyId);
            var position = _mapper.Map<PositionEntity>(dto);
            position.MoneyLower = 0;
            position.MoneyUpper = 0;
            position.User = user;
            position.Company = company;
            return _repository.Create(position);
        }
        public void UpdateMoney(UpdatePositionDto dto)
        {
            var position = _repository.GetByIdOrDefault(dto.Id);
            if (position != null)
            {
                position.MoneyLower = dto.MoneyLower;
                position.MoneyUpper = dto.MoneyUpper;
                _repository.Update(position);
            }
        }
        public void UpdateSetDenied(Guid id, Guid userId)
        {
            var position = _repository.GetByIdOrDefault(id);
            if (position != null)
            {
                position.DenialReceived = true;
                position.OfferReceived = false;
                _repository.Update(position);
            }
        }
        public void UpdateSetOffered(Guid id, Guid userId)
        {
            var position = _repository.GetByIdOrDefault(id);
            if (position != null)
            {
                position.OfferReceived = true;
                position.DenialReceived = false;
                _repository.Update(position);
            }
        }
        public void UpdateComment(UpdateCommentDto dto)
        {
            var position = _repository.Get(e => e.Id == dto.Id && e.UserId == dto.UserId).FirstOrDefault();
            if (position != null)
            {
                position.Comment = dto.Comment;
                _repository.Update(position);
            }
        }
        public void UpdateCity(UpdatePositionDto dto)
        {
            var position = _repository.GetByIdOrDefault(dto.Id);
            if (position != null)
            {
                position.City = dto.City;
                _repository.Update(position);
            }
        }
    }
}
