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
    public class CompanyService : BaseDbService<CompanyEntity>, ICompanyService
    {

        public CompanyService(IRepository<CompanyEntity> repository) :base(repository)
        {
        }

        public void CreateCompany(string name)
        {
            var company = new CompanyEntity() { Id = Guid.NewGuid(), Name = name, Rating = 5 };
            _repository.Create(company);
        }
        public void RateCompany(Guid id, short newRate)
        {
            var countOfRates = 10;
            var company = Get(id);
            company.Rating = Convert.ToInt16(Math.Round(company.Rating - (company.Rating / (countOfRates * 1.0)) + (newRate / (countOfRates * 1.0))));
            _repository.Update(company);
        }
    }
}
