using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.Interfaces;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewsApp.Core.Services
{
    public class InterviewService : BaseDbService<InterviewEntity>, IInterviewService
    {
        private readonly IRepository<UserEntity> _userRepository;

        public InterviewService(IRepository<InterviewEntity> repository, IRepository<UserEntity> userRepository) :base(repository)
        {
            _userRepository = userRepository;
        }
        public override IEnumerable<InterviewEntity> Get()
        {
            return new List<InterviewEntity>();
        }
        public IEnumerable<InterviewEntity> GetByUserId(Guid userId)
        { 
            return (IEnumerable<InterviewEntity>)_userRepository.GetByIdOrDefault(userId)?.Positions.Select(p => p.Interviews);
        }

        public void CreateInterview(CreateInterviewDto dto)
        {
            var interview = new InterviewEntity() { Id = Guid.NewGuid(), Date = dto.Date, Name = dto.Name, PositionId = dto.PositionId };
            _repository.Create(interview);
        }
    }
}
