using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.Interfaces;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Linq;

namespace InterviewsApp.Core.Services
{
    public class PositionService : BaseDbService<PositionEntity>, IPositionService
    {
        protected readonly IRepository<UserEntity> _userRepository;
        protected readonly IRepository<CompanyEntity> _companyRepository;
        public PositionService(IRepository<PositionEntity> repository, IRepository<UserEntity> userRepository, IRepository<CompanyEntity> companyRepository) : base(repository)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
        }
        public void CreatePosition(CreatePositionDto dto)
        {
            var user = _userRepository.GetByIdOrDefault(dto.UserId);
            var company = _companyRepository.GetByIdOrDefault(dto.CompanyId);
            var position = new PositionEntity()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                MoneyLower = 0,
                MoneyUpper = 0,
                City = dto.City,
                CompanyId = dto.CompanyId,
                UserId = dto.UserId
            };
            position.User = user;
            position.Company = company;
            _repository.Create(position);
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
        public void UpdateSetDenied(Guid id)
        {
            var position = _repository.GetByIdOrDefault(id);
            if (position != null)
            {
                position.DenialReceived = true;
                position.OfferReceived = false;
                _repository.Update(position);
            }
        }
        public void UpdateSetOffered(Guid id)
        {
            var position = _repository.GetByIdOrDefault(id);
            if (position != null)
            {
                position.OfferReceived = true;
                position.DenialReceived = false;
                _repository.Update(position);
            }
        }
    }
}
