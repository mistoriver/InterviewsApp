using AutoMapper;
using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Interfaces;
using InterviewsApp.Core.Models;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Linq;

namespace InterviewsApp.Core.Services
{
    public class CompanyService : BaseDbService<CompanyEntity, CompanyDto>, ICompanyService
    {

        private readonly IRepository<PositionEntity> _positionRepository;
        public CompanyService(IRepository<CompanyEntity> repository, IRepository<PositionEntity> positionRepository, IMapper mapper) : base(repository, mapper)
        {
            _positionRepository = positionRepository;
        }

        public Response<Guid> CreateCompany(CreateCompanyDto dto)
        {
            var company = _mapper.Map<CompanyEntity>(dto);
            company.Rating = 50;
            return new Response<Guid>(_repository.Create(company));
        }
        public Response<short> RateCompany(Guid id, Guid userId, short newRate)
        {
            var company = _repository.GetByIdOrDefault(id);
            var positions = _positionRepository.Get(p => p.UserId == userId && p.CompanyId == id).ToList();
            if (positions != null && positions.Count() > 0)
            {
                var oldRate = positions.FirstOrDefault().CompanyRate;
                var newRating = CalculateNewRating(company, newRate, oldRate);
                company.Rating = newRating;
                positions.ForEach(p => p.CompanyRate = newRate);
                _positionRepository.UpdateRange(positions.ToArray());
                _repository.Update(company);
                return new Response<short> (newRating);
            }
            return new Response<short>("Компании, которую вы пытаетесь оценить, не существует");
        }

        public Response<short> GetCompanyRate(Guid id, Guid userId)
        {
            var comp = _positionRepository.Get(p => p.UserId == userId && p.CompanyId == id).FirstOrDefault();
            if (comp != null)
                return new Response<short>(comp.CompanyRate);
            return  new Response<short>("Компании не существует");
        }

        private short CalculateNewRating(CompanyEntity company, short newRate, short oldRate = 0)
        {
            if (newRate >= 0 && newRate <= 100)
            {
                var countOfRates = 10;
                if (company != null)
                {
                    var companyRating = company.Rating;
                    var substract = oldRate == 0 ? (companyRating / (countOfRates * 1f)) : oldRate;
                    var rating = companyRating - substract/ (countOfRates * 1f) + (newRate / (countOfRates * 1f));
                    bool positiveRate = companyRating / (countOfRates * 1f) < newRate / (countOfRates * 1f);

                    return Convert.ToInt16(positiveRate ? Math.Ceiling(rating) : Math.Floor(rating));
                }
            }
            return 0;
        }
    }
}
