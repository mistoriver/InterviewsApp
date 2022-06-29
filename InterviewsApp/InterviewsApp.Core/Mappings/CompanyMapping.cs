using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Data.Models.Entities;

namespace InterviewsApp.Core.Mappings
{
    public class CompanyMapping :BaseMapping<CompanyEntity, CompanyDto>
    {
        public CompanyMapping() : base()
        {
            CreateMap<CreateCompanyDto, CompanyEntity>();
        }
    }
}
