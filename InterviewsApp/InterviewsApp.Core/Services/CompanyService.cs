﻿using AutoMapper;
using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Interfaces;
using InterviewsApp.Core.Models;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewsApp.Core.Services
{
    public class CompanyService : BaseDbService<CompanyEntity, CompanyDto>, ICompanyService
    {

        private readonly IRepository<PositionEntity> _positionRepository;
        public CompanyService(IRepository<CompanyEntity> repository, IRepository<PositionEntity> positionRepository, IMapper mapper) : base(repository, mapper)
        {
            _positionRepository = positionRepository;
        }

        public async Task<Response<Guid>> CreateCompany(CreateCompanyDto dto)
        {
            var company = _mapper.Map<CompanyEntity>(dto);
            company.Rating = 50;
            return new Response<Guid>(await _repository.Create(company));
        }
        public async Task<Response<short>> RateCompany(Guid id, Guid userId, short newRate)
        {
            var company = await _repository.GetByIdOrDefault(id);
            if (company != null)
            {
                var positions = (await _positionRepository.Get(p => p.UserId == userId && p.CompanyId == id)).ToList();
                if (positions != null && positions.Count() > 0)
                {
                    var oldRate = positions.FirstOrDefault().CompanyRate;
                    var newRating = CalculateNewRating(company, newRate, oldRate);
                    company.Rating = newRating;
                    positions.ForEach(p => p.CompanyRate = newRate);
                    await _positionRepository.UpdateRange(positions.ToArray());
                    await _repository.Update(company);
                    return new Response<short>(newRating);
                }
            }
            return new Response<short>("Loc.Message.NoSuchCompanyToRate");
        }

        public async Task<Response<short>> GetUserCompanyRate(Guid id, Guid userId)
        {
            var comp = (await _positionRepository.Get(p => p.UserId == userId && p.CompanyId == id)).FirstOrDefault();
            if (comp != null)
                return new Response<short>(comp.CompanyRate);
            return  new Response<short>("Loc.Message.NoSuchCompany");
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
            return oldRate;
        }
    }
}
