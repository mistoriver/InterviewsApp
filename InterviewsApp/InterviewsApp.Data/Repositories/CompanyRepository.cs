using InterviewsApp.Data.Abstractions;
using InterviewsApp.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewsApp.Data.Repositories
{
    /// <summary>
    /// Репозиторий для доступа к объектам сущности <see cref="CompanyEntity"/> в бд
    /// </summary>
    public class CompanyRepository : BaseRepository<CompanyEntity>
    {
        public CompanyRepository(InterviewsContext context) : base(context)
        { }

        public async Task<bool> Exists(string companyId)
        {
            if (Guid.TryParse(companyId, out var comIdConv))
                return await AppContext.Companies.AnyAsync(c => c.Id == comIdConv);
            return false;
        }
    }
}
