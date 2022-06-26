using AutoMapper;
using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Interfaces;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.Services
{
    public class CompanyService : BaseDbService<CompanyEntity, CompanyDto>, ICompanyService
    {

        public CompanyService(IRepository<CompanyEntity> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public void CreateCompany(CreateCompanyDto dto)
        {
            var company = _mapper.Map<CompanyEntity>(dto);
            company.Rating = 50;
            _repository.Create(company);
        }
        public short RateCompany(Guid id, short newRate)
        {
            if (newRate >= 0 && newRate <= 100)
            {
                var countOfRates = 10;
                var company = _repository.GetByIdOrDefault(id);
                if (company != null)
                {
                    var rating = company.Rating - (company.Rating / (countOfRates * 1f)) + (newRate / (countOfRates * 1f));
                    bool positiveRate = company.Rating / (countOfRates * 1f) < newRate / (countOfRates * 1f);

                    company.Rating = Convert.ToInt16(positiveRate ? Math.Ceiling(rating) : Math.Floor(rating));
                    _repository.Update(company);

                    return company.Rating;
                }
            }
            return 0;
        }
    }
}
