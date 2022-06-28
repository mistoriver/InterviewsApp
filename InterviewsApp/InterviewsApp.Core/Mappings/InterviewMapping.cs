using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Data.Models.Entities;

namespace InterviewsApp.Core.Mappings
{
    public class InterviewMapping : BaseMapping<InterviewEntity, InterviewDto>
    {
        public InterviewMapping() : base()
        {
            CreateMap<CreateInterviewDto, InterviewEntity>();
        }
    }
}
