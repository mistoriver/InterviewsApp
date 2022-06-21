using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.Interfaces;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;

namespace InterviewsApp.Core.Services
{
    public class InterviewService : IInterviewService
    {
        private readonly IRepository<InterviewEntity> _repository;

        public InterviewService(IRepository<InterviewEntity> repository)
        {
            _repository = repository;
        }
        public IEnumerable<InterviewEntity> Get(Guid id)
        {
            if (id != Guid.Empty)
                return _repository.GetByPredicate(entity => entity.Id.Equals(id));
            return _repository.GetByPredicate(e => true);
        }

        public void CreateInterview(CreateInterviewDto dto)
        {

        }
    }
}
