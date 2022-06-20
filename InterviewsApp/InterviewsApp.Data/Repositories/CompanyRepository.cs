using InterviewsApp.Data.Abstractions;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Linq;

namespace InterviewsApp.Data.Repositories
{
    /// <summary>
    /// Репозиторий для доступа к объектам сущности <see cref="CompanyEntity"/> в бд
    /// </summary>
    public class CompanyRepository : BaseRepository<CompanyEntity>
    {
        public CompanyRepository(InterviewsContext context) : base(context)
        { }

        public bool CompanyExists(string companyId)
        {
            if (Guid.TryParse(companyId, out var comIdConv))
                return AppContext.Companies.Any(c => c.Id == comIdConv);
            return false;
        }
    }
}
